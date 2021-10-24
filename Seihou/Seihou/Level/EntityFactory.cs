using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	/// <summary>
	/// Class used by the level manager to create and add entities to the world.
	/// </summary>
	class EntityFactory
	{
		public record IndexedType
		{
			public Type Type { get; init; }
			public (ParameterInfo info, string param)[] Parameters { get; init; }
		}

		public readonly EntityManager _entityManager;
		private readonly Dictionary<Type, object> _dependencies = new();
		private Dictionary<string, IndexedType> _index;

		public EntityFactory(EntityManager manager)
		{
			_entityManager = manager;
		}

		public IReadOnlyDictionary<string, IndexedType> GetIndexedItems() => _index;

		public void AddDependency<Interface, Implementation>(Implementation obj) where Implementation : Interface
		{
			_dependencies.Add(typeof(Interface), obj);
		}

		public void AddDependency(object obj)
		{
			_dependencies.Add(obj.GetType(), obj);
		}

		public void Index()
		{
			Index(Assembly.GetExecutingAssembly());
		}

		public void Index(Assembly assembly)
		{
			_index = assembly.GetTypes()
				.Where(IndexFilter)
				.ToDictionary(t => t.Name, k => ToIndexedType(k));
		}


		public static bool IndexFilter(Type type)
		{
			return
				type.IsAssignableTo(typeof(Entity)) &&
				!type.IsAbstract &&
				!type.IsInterface &&
				type.IsClass;
		}

		public void Spawn(EntityCreationData creationData)
		{
			_entityManager.AddEntity(Create(creationData));
		}

		public Entity Create(EntityCreationData creationData)
		{
			if (!_index.TryGetValue(creationData.EntityName, out var indexedItem))
			{
				throw new InvalidOperationException($"Type is not indexed '{creationData?.EntityName}'");
			}

			object[] parameterValues = new object[indexedItem.Parameters.Length];

			for (int i = 0; i < indexedItem.Parameters.Length; i++)
			{
				var (info, param) = indexedItem.Parameters[i];
				var value = CreateParamterValue(info, param, creationData);
				parameterValues[i] = value;
			}

			var instance = Activator.CreateInstance(indexedItem.Type, parameterValues.ToArray());
			return (Entity)instance;
		}

		private static IndexedType ToIndexedType(Type type)
		{
			var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
			var constructor = constructors
				.FirstOrDefault(c => c.GetCustomAttribute<MainCtorAttribute>() != null) 
				?? constructors.First();

			var parameters = constructor.GetParameters();

			return new IndexedType
			{
				Type = type,
				Parameters = parameters
					.Select(p => (p, p.GetCustomAttribute<ParamAttribute>(true)?.Name))
					.ToArray(),
			};
		}

		private object CreateParamterValue(ParameterInfo parameter, string param, EntityCreationData properties)
		{
			if (param == PositionAttribute.PARAM_NAME)
			{
				return properties.Position;
			}

			Debug.Assert(parameter.Name != "pos", "Detected potential missing PoistionAttribute");

			if (_dependencies.TryGetValue(parameter.ParameterType, out object dependencyValue))
			{
				return dependencyValue;
			}

			if (param != null)
			{
				if (properties.Properties.TryGetValue(param, out string propertyValue))
				{
					return ParameterParser.Parse(parameter.ParameterType, propertyValue);
				}

				throw new InvalidOperationException($"Missing paramter '{param}' in map properties needed for type '{parameter?.Member?.DeclaringType?.Name}'");
			}

			throw new InvalidOperationException($"Could not find dependency '{parameter?.ParameterType}'({parameter?.Name}) needed to create '{parameter?.Member?.DeclaringType?.Name}'");
		}
	}
}

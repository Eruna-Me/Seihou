using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	class EntityFactory
	{
		private readonly SpriteBatch _spriteBatch;
		private readonly EntityManager _entityManager;
		private Dictionary<string, Type> _index;

		public EntityFactory(SpriteBatch spriteBatch, EntityManager entityManager)
		{
			_spriteBatch = spriteBatch;
			_entityManager = entityManager;
		}

		public void Index()
		{
			var asm = Assembly.GetExecutingAssembly();
			_index = asm.GetTypes()
				.Where(t => t.IsAssignableTo(typeof(Entity)))
				.ToDictionary(t => t.Name);
		}

		public Entity Create(Vector2 position, string name)
		{
			return (Entity)Activator.CreateInstance(_index[name], new object[]
			{
				position,
				_spriteBatch,
				_entityManager
			});
		}
	}
}

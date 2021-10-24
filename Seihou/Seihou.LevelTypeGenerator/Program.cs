using System;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace Seihou.LevelTypeGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			var path = args[0];
			var random = new Random(5);
			var assembly = AppDomain.CurrentDomain.GetAssemblies().First(t => t.GetName().Name == "Seihou");

			EntityFactory factory = new(null);
			factory.Index(assembly);

			using XmlWriter writer = XmlWriter.Create(path, new XmlWriterSettings { Indent = true });
			writer.WriteStartDocument();
			writer.WriteStartElement("objecttypes");

			foreach(var entity in factory.GetIndexedItems())
			{
				var color = string.Format("#{0:X6}", random.Next(0x1000000));

				writer.WriteStartElement("objecttype");
				writer.WriteAttributeString("name", entity.Key);
				writer.WriteAttributeString("color", color);

				foreach(var (info, param) in entity.Value.Parameters)
				{
					if (param != null && param != "Position")
					{
						writer.WriteStartElement("property");
						writer.WriteAttributeString("name", param);
						writer.WriteEndElement();
					}
				}

				writer.WriteEndElement();
			}

			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Flush();
		}
	}
}

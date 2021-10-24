using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	static class ParameterParser
	{
		public static object Parse(Type type, string parameter)
		{
			var typeSwitch = new Dictionary<Type, Func<string, object>> {
				{ typeof(Vector2), ParseVector2},
				{ typeof(Color), ParseColor},				
			};

			if (typeSwitch.TryGetValue(type, out var func))
			{
				return func(parameter);
			}

			return ParseType(parameter, type);
		}

		private static object ParseType(string str, Type type)
		{
			return Convert.ChangeType(str,type,CultureInfo.InvariantCulture);
		}

		private static object ParseColor(string str)
		{
			var parts = str.Split(',').Select(p => byte.Parse(p)).ToArray();
			return new Color(parts[0], parts[1], parts[2], parts.Length > 3 ? parts[3] : 255);
		}

		private static object ParseVector2(string str)
		{
			var parts = str.Split(',').Select(p => float.Parse(p)).ToArray();
			return new Vector2(parts[0], parts[1]);
		}
	}
}

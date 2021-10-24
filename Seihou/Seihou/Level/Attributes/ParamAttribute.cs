using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	/// <summary>
	/// Name used to inject properties from the level editor.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter)]
	class ParamAttribute : Attribute
	{
		public string Name { get; set; }

		public ParamAttribute(string name)
		{
			Name = name;
		}
	}
}

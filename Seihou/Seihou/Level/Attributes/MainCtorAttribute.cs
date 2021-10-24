using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	/// <summary>
	/// Let the level manager know that this constructor should be used.
	/// Should only be used if there is more than one public constructor.
	/// </summary>
	[AttributeUsage(AttributeTargets.Constructor)]
	class MainCtorAttribute : Attribute
	{
	}
}

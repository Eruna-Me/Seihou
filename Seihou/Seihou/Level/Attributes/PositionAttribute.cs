using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	/// <summary>
	/// Constructor parameter for position. Used by level editor.
	/// </summary>
	class PositionAttribute : ParamAttribute
	{
		public const string PARAM_NAME = "Position";
		public PositionAttribute() : base(PARAM_NAME)
		{
		}
	}
}

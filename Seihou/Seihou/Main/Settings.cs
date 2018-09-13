using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
	static class Settings
	{
		//	SCREENSIZE
		public static readonly int screenWidth = 800;
		public static readonly int screenHeight = 600;

		//	CONTROLS
		public static Keys upKey = Keys.Up;
		public static Keys downKey = Keys.Down;
		public static Keys leftKey = Keys.Left;
		public static Keys rightKey = Keys.Right;
		public static Keys slowKey = Keys.LeftShift;
	}
}

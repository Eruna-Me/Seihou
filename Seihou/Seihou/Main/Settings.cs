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
		//	CONTROLS
		public static Keys upKey = Keys.Up;
		public static Keys downKey = Keys.Down;
		public static Keys leftKey = Keys.Left;
		public static Keys rightKey = Keys.Right;
		public static Keys slowKey = Keys.LeftShift;
        public static Keys shootKey = Keys.Z;

		//	DIFFICULTY
		public enum Difficulty{ easy, normal, hard, usagi }; // Uesugi Kenshin >> Usagi Kenshin?
		public static Difficulty difficulty = Difficulty.easy;
		public static int startingLives = 6;
		public static int startingBombs = 0;

		//	GRAPHICS
        public static bool SimpleGraphics = false;
	}
}

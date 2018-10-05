using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
	static class Global
	{
		//	SCREENSIZE
		public const int screenWidth = 800;  // 1152	648, 1024	576
		public const int screenHeight = 600;
        public const int outOfScreenMargin = 100;
		public const int uiWidth = screenWidth / 2;
		public static readonly int playingFieldWidth = screenWidth - uiWidth;
        public static readonly Vector2 FpsCounterPos = new Vector2(playingFieldWidth+10, screenHeight - 100);
        public static readonly Vector2 EntCounterPos = new Vector2(playingFieldWidth+10, screenHeight - 60);
        public static readonly Vector2 Center = new Vector2(playingFieldWidth / 2, screenHeight / 2);
        public const int spawnHeight = -50;

		//	BUTTONS
		public static Keys PauseKey1 = Keys.F10;
		public static Keys PauseKey2 = Keys.Escape;

		//	VARIOUS
        public static Random random = new Random();
		public static Player player = null;
		public const bool drawCollisionBoxes = false;

        public enum Faction
        {
            friendly,
            enemy,
            neutral,
            noFaction
        }

        public static float Choose(float[] floats) => floats[random.Next(0, floats.Length)];

        //Normalize vector
        public static Vector2 Normalize(Vector2 vec)
        {
            //TODO: find a cleaner way to do this
            if (vec.X == 0)
                return new Vector2(0, Math.Sign(vec.Y));

            float mag = (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
                return new Vector2(vec.X/mag,vec.Y/mag);
        }

		public static float VtoD(Vector2 vector) => (float)Math.Atan2(vector.Y, vector.X);
	}
}

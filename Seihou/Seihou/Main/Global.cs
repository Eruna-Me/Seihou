using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Seihou
{
	static class Global
	{
		//	SCREENSIZE
		public static readonly int screenWidth = 800;
		public static readonly int screenHeight = 600;
		public static readonly int outOfScreenMargin = 100;
		public static readonly int uiWidth = 300;
		public static readonly int playingFieldWidth = screenWidth - uiWidth;
        public const int spawnHeight = -50;

        public static Random random = new Random();

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

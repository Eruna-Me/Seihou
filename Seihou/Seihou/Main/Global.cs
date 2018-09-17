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

        public enum Faction
        {
            friendly,
            enemy,
            neutral,
            noFaction
        }

        //Degrees to Vector
        public static Vector2 DtoV(float degrees)
        {
            return new Vector2((float)Math.Cos(degrees * Math.PI / 180),(float)Math.Sin(degrees * 180 / Math.PI));
        }

        //Vector to degrees
        public static float VtoD(Vector2 vec)
        {
            return (float)(Math.Atan2(vec.Y,vec.X) * 180 / Math.PI);
        }

        //Point
        public static Vector2 PointTo(Vector2 vec,float direction)
        {
            return new Vector2();
        }

        //Normalize vector ish
        public static Vector2 Normalize(Vector2 vec)
        {
            //TODO: find a faster way to do this
            if (vec.X == 0)
                return new Vector2(0, Math.Sign(vec.Y));

            if (vec.Y == 0)
                return new Vector2(Math.Sign(vec.X), 0);

            float max = Math.Max(vec.X, vec.Y);
                return new Vector2(vec.X/max,vec.Y/max);
        }   
    }
}

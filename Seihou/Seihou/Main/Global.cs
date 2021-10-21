using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	static class Global
	{
        //	SCREENSIZE
        public const int screenWidth = 1280;
        public const int screenHeight = 720;
        public const int outOfScreenMargin = 100;
		public const int uiWidth = screenWidth * 3 / 8;
		public static readonly int playingFieldWidth = screenWidth - uiWidth;
        public static readonly Vector2 FpsCounterPos = new Vector2(playingFieldWidth+10, screenHeight - 100);
        public static readonly Vector2 EntCounterPos = new Vector2(playingFieldWidth+10, screenHeight - 60);
        public static readonly Vector2 Center = new Vector2(playingFieldWidth / 2, screenHeight / 2);
        public const int spawnHeight = -100;

		//	BUTTONS
		public static Keys PauseKey1 = Keys.F10;
		public static Keys PauseKey2 = Keys.Escape;

		//	VARIOUS
		public static bool keyMode = true;
		public static int selectedButton = 0;
		public static int buttonCount = 0;
		public static Random random = new Random();
		public static Player player = null;
        public static Color gameBackgroundColor = new Color(100,200,250);

        public static float Choose(float[] floats) => floats[random.Next(0, floats.Length)];

        //Normalize vector
        public static Vector2 Normalize(Vector2 vec)
        {
            float mag = (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
            if (mag == 0) return new Vector2(0, 0); 
            return new Vector2(vec.X != 0 ? vec.X/mag : 0,vec.Y != 0 ? vec.Y/mag : 0);
        }

		public static float VtoD(Vector2 vector) => (float)Math.Atan2(vector.Y, vector.X);

		public static void SpreadShot(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, float bulletSpeed, string texture, float direction, float spread, int amount)
		{
			if (amount %  2 != 0)
				em.AddEntity(new EnemyBullet(pos, sb, em, owner, new Vector2((float)Math.Cos(direction) * bulletSpeed, (float)Math.Sin(direction) * bulletSpeed), texture));

			for (int i = amount / 2; i > 0; i--)
			{
				em.AddEntity(new EnemyBullet(pos, sb, em, owner, new Vector2((float)Math.Cos(direction - Math.PI / (spread / i)) * bulletSpeed, (float)Math.Sin(direction - Math.PI / (spread / i)) * bulletSpeed), texture));
				em.AddEntity(new EnemyBullet(pos, sb, em, owner, new Vector2((float)Math.Cos(direction + Math.PI / (spread / i)) * bulletSpeed, (float)Math.Sin(direction + Math.PI / (spread / i)) * bulletSpeed), texture));
			}
		}

		public static bool OnScreen(Vector2 pos)
		{
			if (pos.Y < 0 || pos.Y > screenHeight || pos.X < 0 || pos.X > playingFieldWidth)
			{
				return false;
			}
			return true;
		}
	}
}

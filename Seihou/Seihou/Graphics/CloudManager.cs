using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Seihou
{
	static class CloudManager
	{
		static public Color color = Color.White;
		static public float speed = 50.0f;
		static public float minAlpha = 0.0f;
		static public float maxAlpha = 0.8f;
		static public float spawnDelay = 0.0f;
		static public float maxSpawnDelay = 0.01f;
		static public float deltaSpawnDelay = 0.1f;
		static public float deltaSpeedVariance = 10.0f;

		public static void Update(GameTime gt, SpriteBatch sb, EntityManager em)
		{	
			while (spawnDelay < gt.ElapsedGameTime.TotalSeconds)
			{
				float alpha = (float)Global.random.NextDouble() * (maxAlpha - minAlpha) - minAlpha;
				float deltaSpeed = (float)Global.random.NextDouble() * deltaSpeedVariance;
				Vector2 pos = new Vector2(Global.random.Next(-Global.outOfScreenMargin, Global.playingFieldWidth + Global.outOfScreenMargin), Global.spawnHeight);
				em.AddEntity(new Cloud(pos, sb, em, "Cloud1", alpha, deltaSpeed));
				spawnDelay += maxSpawnDelay + (float)Global.random.NextDouble() * deltaSpawnDelay;
			}

			spawnDelay -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;
		}

		public static void FillScreen(SpriteBatch sb, EntityManager em)
		{
			float y = Global.spawnHeight;
			while (y < Global.screenHeight + Global.outOfScreenMargin)
			{
				float alpha = (float)Global.random.NextDouble() * (maxAlpha - minAlpha) - minAlpha;
				float deltaSpeed = (float)Global.random.NextDouble() * deltaSpeedVariance;
				Vector2 pos = new Vector2(Global.random.Next(-Global.outOfScreenMargin, Global.playingFieldWidth + Global.outOfScreenMargin), y);
				em.AddEntity(new Cloud(pos, sb, em, "Cloud1", alpha, deltaSpeed));
				y += maxSpawnDelay + (float)Global.random.NextDouble() * deltaSpawnDelay * (speed + (float)Global.random.NextDouble() * deltaSpeedVariance);
			}
		}
	}
}

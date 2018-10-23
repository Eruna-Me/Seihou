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
		static public float speed = 100.0f;
		static public float minAlpha = 0.0f;
		static public float maxAlpha = 1.0f;
		static public float spawnChance = 1.0f;
		static public float deltaSpeedVariance = 20.0f;

		public static void Update(GameTime gt, SpriteBatch sb, EntityManager em)
		{	
			Vector2 pos = new Vector2(Global.random.Next(-Global.outOfScreenMargin, Global.playingFieldWidth + Global.outOfScreenMargin),Global.spawnHeight);
			float alpha = (float)Global.random.NextDouble() * (maxAlpha - minAlpha) - minAlpha;
			float deltaSpeed = (float)Global.random.NextDouble() * deltaSpeedVariance;

			em.AddEntity(new Cloud(pos, sb, em, "Cloud1", alpha, deltaSpeed));
		}
	}
}

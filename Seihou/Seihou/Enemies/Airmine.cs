﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Seihou
{
	class Airmine : Enemy
    {
        private const float fallSpeed = 40.0f;
        private const float maxBulletSpeed = 100.0f;
		private const float minBulletSpeed = 100.0f;
		private readonly int bullets = 10;

        public Airmine([Position]Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
            texture = "Mine";
            ec = EntityManager.EntityClass.enemy;
            size = 40; 
            speed.Y = fallSpeed;
			hp = 1;
			if (Settings.GetDifficulty() == Settings.Difficulty.easy)	bullets = 3;
			if (Settings.GetDifficulty() == Settings.Difficulty.normal) bullets = 6;
			if (Settings.GetDifficulty() == Settings.Difficulty.usagi)	bullets = 20;

		}

		public override void OnDamaged(Entity by, int damage)
		{
			if (Global.OnScreen(pos))
			{
				SoundHelper.PlayRandom("ExplosionShort");

				for (int i = 0; i < bullets; i++)
				{
					float s = (float)(Global.random.NextDouble() * (maxBulletSpeed - minBulletSpeed) + minBulletSpeed);

					float d = (float)(Global.random.NextDouble() * Math.PI * 2);

					Vector2 bSpeed = new Vector2((float)Math.Cos(d) * s, (float)Math.Sin(d) * s);

					em.AddEntity(new EnemyBullet(pos, sb, em, this, bSpeed));
				}
			}
			hp -= damage;
			em.RemoveEntity(this);
			Global.player.score += scoreOnKilled;
		}

		public override void Update(GameTime gt)
        {
            pos += speed * gt.Time();

            base.Update(gt);
        }
    }
}

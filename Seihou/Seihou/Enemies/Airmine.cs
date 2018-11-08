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
    class Airmine : Enemy
    {
        private const float fallSpeed = 40.0f;
        private const float maxBulletSpeed = 200.0f;
		private const float minBulletSpeed = 50.0f;
		private const int bullets = 1000;

        public Airmine(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
            texture = "Mine";
            ec = EntityManager.EntityClass.enemy;
            size = 40; 
            speed.Y = fallSpeed;
			hp = 1;
        }

		public override void OnDamaged(Entity by, int damage)
		{
			
			for (int i = 0; i < bullets; i++)
			{
				float s = (float)(Global.random.NextDouble() * (maxBulletSpeed - minBulletSpeed) + minBulletSpeed);

				float d = (float)(Global.random.NextDouble() * Math.PI * 2);

				Vector2 bSpeed = new Vector2((float)Math.Cos(d) * s,(float)Math.Sin(d) * s);

				em.AddEntity(new EnemyBullet(pos, sb, em, this, bSpeed));
			}

			hp = 0;
			em.RemoveEntity(this);
		}

		public override void Update(GameTime gt)
        {
            pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

            base.Update(gt);
        }
    }
}

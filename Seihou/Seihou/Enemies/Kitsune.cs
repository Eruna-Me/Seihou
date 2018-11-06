using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//standard faller
namespace Seihou
{
    class Kitsune : Enemy
    {
        private const float fallSpeed = 50.0f;
		private const float bulletSpeed = 350.0f;
		private float fireDelay = 0.5f;
		private float maxFireDelay = 2.0f;

		public Kitsune(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
		{
			texture = "Kitsune";
			ec = EntityManager.EntityClass.enemy;
			size = 24;
			speed.Y = fallSpeed;
			hp = 3;
		}

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

			if (fireDelay <= 0)
			{
				em.AddEntity(new EnemyBullet(pos, sb, em, this, Global.Normalize(em.GetPlayer().pos - pos) * bulletSpeed));
				fireDelay = maxFireDelay;
			}

			fireDelay -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;
		}
    }
}

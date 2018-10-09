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
    class Samurai : Enemy
    {
        private const float fallSpeed = 90.0f;
		private const float bulletSpeed = 400.0f;
		private float fireDelay = 2.0f;
		private float maxFireDelay = 2.0f;

		public Samurai(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
		{
			texture = "Samurai";
			ec = EntityManager.EntityClass.enemy;
			size = 24;
			speed.Y = fallSpeed;
			hp = 3;

			if (fireDelay <= 0 && Settings.difficulty == Settings.Difficulty.usagi)
				maxFireDelay = 1.25f;
		}

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

			if (fireDelay <= 0 && Settings.difficulty >= Settings.Difficulty.hard)
			{
				em.AddEntity(new EnemyBullet(pos, sb, em, this, Global.Normalize(em.GetPlayer().pos - pos) * bulletSpeed));
				fireDelay = maxFireDelay;
			}

			fireDelay -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;
		}
    }
}

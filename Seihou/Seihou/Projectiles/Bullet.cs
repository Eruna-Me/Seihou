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
	class Bullet : Projectile
	{
		public Bullet(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em, owner)
		{
            size = 10;

            this.speed = speed;
		}

		public override void Update(GameTime gt)
		{
			base.Update(gt);
			pos += speed * (float)gt.ElapsedGameTime.TotalSeconds; 

            Entity c = em.Touching(this);

            if (c != null)
            {
                if (c.faction != owner.faction)
                {
                    c.Damage(owner, 1);
                }
            }
		}


		public override void Draw(GameTime gt)
		{
			MonoGame.Primitives2D.DrawCircle(sb,pos, size, 100, Color.Blue, 1);
		}
	}
}

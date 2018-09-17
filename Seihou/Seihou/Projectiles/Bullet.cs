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
            collision = true;
            this.speed = speed;
		}

		public override void Update(GameTime gt)
		{
			base.Update(gt);
			pos += speed * (float)gt.ElapsedGameTime.TotalSeconds; 

            Entity c = em.Touching(this);

            if (c != null)
            {
                if (c.faction != owner.faction )
                {
                    c.Damage(owner, 1);
                }
            }
		}

		public override void Draw(GameTime gt)
		{
			double angle = Math.Atan2(speed.Y, speed.X) + Math.PI / 2; // TODO fix dart texture so I don't have to rotate it. :S
			sb.Draw(SpriteManager.textures["Dart1"], pos, null, Color.White, (float)angle, new Vector2(0,0), 1.0f, SpriteEffects.None, 0f);
		}
	}
}

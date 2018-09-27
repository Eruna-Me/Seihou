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
	abstract class PlayerProjectile : Entity
	{
        protected readonly Entity owner;

		protected PlayerProjectile(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner) : base(pos, sb, em)
		{
            this.owner = owner;
		}

		public override void Update(GameTime gt)
		{
			pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

			Entity c = em.Touching(this, EntityManager.EntityClass.enemy);

			if (c != null)
			{
				if (c.hp > 0 && hp > 0)
				{
					hp--;
					c.OnDamaged(owner, 1);
					em.RemoveEntity(this);
				}
			}

			if (pos.Y + Global.outOfScreenMargin < 0 || pos.Y > Global.screenHeight + Global.outOfScreenMargin || pos.X + Global.outOfScreenMargin < 0 || pos.X > Global.screenWidth + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}
		}

		public override void Draw(GameTime gt)
		{
			ResourceManager.DrawAngledTexture(sb, texture, pos, speed);
			if (Global.drawCollisionBoxes) MonoGame.Primitives2D.DrawCircle(sb, pos, size, 10, Color.White, 1);
		}
	}
}

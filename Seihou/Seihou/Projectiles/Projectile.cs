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
	abstract class Projectile : Entity
	{
        protected readonly Entity owner;

		protected Projectile(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner) : base(pos, sb, em)
		{
            this.owner = owner;
		}

		public override void Update(GameTime gt)
		{
			if (pos.Y + Global.outOfScreenMargin < 0 || pos.Y > Global.screenHeight + Global.outOfScreenMargin || pos.X + Global.outOfScreenMargin < 0 || pos.X > Global.screenWidth + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}
		}
	}
}

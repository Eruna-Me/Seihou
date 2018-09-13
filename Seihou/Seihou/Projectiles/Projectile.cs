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
		protected float xSpeed;
		protected float ySpeed;

		protected Projectile(float x, float y, SpriteBatch sb, EntityManager em, Entity owner) : base(x, y, sb, em)
		{
			
		}

		public override void Update(GameTime gt)
		{
			if (y + Global.outOfScreenMargin < 0 || y > Global.screenHeight + Global.outOfScreenMargin || x + Global.outOfScreenMargin < 0 || x > Global.screenWidth + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}
		}
	}
}

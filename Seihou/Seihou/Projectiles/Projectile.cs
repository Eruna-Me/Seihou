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

		protected Projectile(float x, float y, SpriteBatch sb, EntityManager em) : base(x, y, sb, em)
		{
			
		}
	}
}

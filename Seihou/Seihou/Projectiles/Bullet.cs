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
		public Bullet(float x, float y, SpriteBatch sb, EntityManager em, float ySpeed, float xSpeed) : base(x, y, sb, em)
		{
			this.xSpeed = xSpeed;
			this.ySpeed = ySpeed;
		}

		public override void Update(GameTime gt)
		{
			x += xSpeed * (float)gt.ElapsedGameTime.TotalSeconds; 
			y += ySpeed * (float)gt.ElapsedGameTime.TotalSeconds; 
		}

		public override void Draw(GameTime gt)
		{
			MonoGame.Primitives2D.DrawCircle(sb, x, y, 20, 100, Color.Blue, 5);
		}
	}
}

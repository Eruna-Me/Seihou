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
		public Bullet(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, float xSpeed, float ySpeed) : base(pos, sb, em, owner)
		{
			this.xSpeed = xSpeed;
			this.ySpeed = ySpeed;
		}

		public override void Update(GameTime gt)
		{
			base.Update(gt);
			pos.X += xSpeed * (float)gt.ElapsedGameTime.TotalSeconds; 
			pos.Y += ySpeed * (float)gt.ElapsedGameTime.TotalSeconds;
		}


		public override void Draw(GameTime gt)
		{
			MonoGame.Primitives2D.DrawCircle(sb,pos, 20, 100, Color.Blue, 5);
		}
	}
}

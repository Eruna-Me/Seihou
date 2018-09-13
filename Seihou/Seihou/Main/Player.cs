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
    class Player : Entity
    {
        private float speed;
		const int size = 50;
		private float fireDelay = 0;
		const float maxFireDelay = 0.1f;

        public Player(float x,float y,SpriteBatch sb, EntityManager em) : base(x, y, sb, em)
        {
            //Init player
        }

        public override void Update(GameTime gt)
        {
			speed = Keyboard.GetState().IsKeyDown(Settings.slowKey) ? 100 : 200 ;

            if (Keyboard.GetState().IsKeyDown(Settings.rightKey))
                x += speed * (float)gt.ElapsedGameTime.TotalSeconds;
			if (Keyboard.GetState().IsKeyDown(Settings.leftKey))
				x -= speed * (float)gt.ElapsedGameTime.TotalSeconds;
			if (Keyboard.GetState().IsKeyDown(Settings.downKey))
				y += speed * (float)gt.ElapsedGameTime.TotalSeconds;
			if (Keyboard.GetState().IsKeyDown(Settings.upKey))
				y -= speed * (float)gt.ElapsedGameTime.TotalSeconds;

			if (Keyboard.GetState().IsKeyDown(Keys.X) && fireDelay <= 0)
			{
				em.AddEntity(new Bullet(x, y, sb, em, 0, -400));

				fireDelay = maxFireDelay;
			}
			fireDelay -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;
		}

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.DrawCircle(sb, x, y, 50, 100, Color.Red, 5);
        }
    }
}

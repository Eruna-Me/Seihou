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
        //Firing
        private float fireDelay = 0;
        private const float maxFireDelay = 0.05f;

        //Player
        private Vector2 speed = new Vector2(0.0f, 0.0f);
        private const float maxSpeed = 5.0f;
		private const int size = 50;
		

        public Player(Vector2 pos,SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
            //Init player
        }

        public void Fire()
        {
            em.AddEntity(new Bullet(pos, sb, em, this, 0, -400));
        }

        public override void Update(GameTime gt)
        {
            KeyboardState kb = Keyboard.GetState();
            bool u = kb.IsKeyDown(Settings.upKey);
            bool r = kb.IsKeyDown(Settings.rightKey);
            bool d = kb.IsKeyDown(Settings.downKey);
            bool l = kb.IsKeyDown(Settings.leftKey);

            speed.X = 0.0f;
            speed.Y = 0.0f;

            //Movement
            speed.X = (Convert.ToInt32(r) - Convert.ToInt32(l));
            speed.Y = (Convert.ToInt32(d) - Convert.ToInt32(u));

            speed = Global.Normalize(speed) * maxSpeed;

            //Fire
            if (Keyboard.GetState().IsKeyDown(Keys.X) && fireDelay <= 0)
			{
                Fire();
				fireDelay = maxFireDelay;
			}
			fireDelay -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;

            pos += speed;
		}

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.DrawCircle(sb, pos, 50, 100, Color.Red, 5);
        }
    }
}

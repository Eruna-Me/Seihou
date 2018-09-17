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
        private const float maxFireDelay = 0.01f;
        private const float bulletSpeed = 500.0f;

        //Player
        private const float maxSpeed = 300.0f;
		

        public Player(Vector2 pos,SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
            size = 30;
            faction = Global.Faction.friendly;
        }

        public void Fire()
        { 
            em.AddEntity(new Bullet(pos, sb, em, this,new Vector2( 0, -bulletSpeed)));
            em.AddEntity(new Bullet(pos, sb, em, this, new Vector2(100, -bulletSpeed)));
            em.AddEntity(new Bullet(pos, sb, em, this, new Vector2(-100, -bulletSpeed)));
            em.AddEntity(new Bullet(pos, sb, em, this, new Vector2(200, -bulletSpeed)));
            em.AddEntity(new Bullet(pos, sb, em, this, new Vector2(-200, -bulletSpeed)));
        }

        public override void Update(GameTime gt)
        {
            KeyboardState kb = Keyboard.GetState();

            bool u = kb.IsKeyDown(Settings.upKey);
            bool r = kb.IsKeyDown(Settings.rightKey);
            bool d = kb.IsKeyDown(Settings.downKey);
            bool l = kb.IsKeyDown(Settings.leftKey);

            //Movement
            speed.X = (Convert.ToInt32(r) - Convert.ToInt32(l)) * maxSpeed;
            speed.Y = (Convert.ToInt32(d) - Convert.ToInt32(u)) * maxSpeed;

            //Fire
            if (Keyboard.GetState().IsKeyDown(Settings.shootKey) && fireDelay <= 0)
			{
                Fire();
				fireDelay = maxFireDelay;
			}
			fireDelay -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;

            if (!((pos.X + speed.X * (float)gt.ElapsedGameTime.TotalSeconds + size > Global.playingFieldWidth) || (pos.X + speed.X * (float)gt.ElapsedGameTime.TotalSeconds - size < 0)))
                pos.X += speed.X * (float)gt.ElapsedGameTime.TotalSeconds;

            if (!((pos.Y + speed.Y * (float)gt.ElapsedGameTime.TotalSeconds + size > Global.screenHeight) || (pos.Y + speed.Y * (float)gt.ElapsedGameTime.TotalSeconds - size < 0)))
                pos.Y += speed.Y * (float)gt.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.DrawCircle(sb, pos, size, 100, Color.Red, 5);
        }
    }
}

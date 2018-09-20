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
		//Survivability
		private float invincibilityTimer = 0;
		private const float maxInvincibilityTimer = 1.0f;
		private const float maxFireDelay = 0.1f;

		//Firing
		private float fireDelay = 0;
        private const float bulletSpeed = 500.0f;
        private const float bulletSpread = 50.0f;

        //Speed
        private const float normalSpeed = 300.0f;
		private const float slowSpeed = 150.0f;

        public Player(Vector2 pos,SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
            size = 30;
            faction = Global.Faction.friendly;
            ec = EntityManager.EntityClass.player;
        }

        public void Fire()
        {
                em.AddEntity(new Bullet(pos, sb, em, this, new Vector2(0 , -bulletSpeed )));
                em.AddEntity(new Bullet(pos, sb, em, this, new Vector2(bulletSpread , -bulletSpeed )));
                em.AddEntity(new Bullet(pos, sb, em, this, new Vector2(-bulletSpread , -bulletSpeed )));
        }

        public override void Update(GameTime gt)
        {
            KeyboardState kb = Keyboard.GetState();

            bool u = kb.IsKeyDown(Settings.upKey);
            bool r = kb.IsKeyDown(Settings.rightKey);
            bool d = kb.IsKeyDown(Settings.downKey);
            bool l = kb.IsKeyDown(Settings.leftKey);
			bool slowMode = kb.IsKeyDown(Settings.slowKey);

			bool s = kb.IsKeyDown(Settings.shootKey);

            //Movement
            speed.X = (Convert.ToInt32(r) - Convert.ToInt32(l)) * (slowMode ? slowSpeed : normalSpeed);
            speed.Y = (Convert.ToInt32(d) - Convert.ToInt32(u)) * (slowMode ? slowSpeed : normalSpeed);

			pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

			if (pos.X + size > Global.playingFieldWidth) pos.X = Global.playingFieldWidth - size;
			if (pos.X - size < 0) pos.X = 0 + size;
			if (pos.Y + size > Global.screenHeight) pos.Y = Global.screenHeight - size;
			if (pos.Y - size < 0) pos.Y = 0 + size;

			//Fire
			if (s && fireDelay <= 0)
			{
                Fire();
				fireDelay = maxFireDelay;
			}
			fireDelay -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;
			invincibilityTimer -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;
		}

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.DrawCircle(sb, pos, size, 100, Color.Red, 5);
        }

		public override void Damage(Entity by, int damage)
		{
			if(invincibilityTimer <= 0)
			{ 
				for (int i = 0; i < 20; i++)
				{
					em.AddEntity(new Particle(pos, sb, em));
				}
				ResetPosition();
				Global.lives--;
				invincibilityTimer = maxInvincibilityTimer;
			}
		}

		void ResetPosition()
		{
			pos = new Vector2(Global.playingFieldWidth / 2 , Global.screenHeight - 50);
		}
	}
}

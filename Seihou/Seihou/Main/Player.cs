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
		//Graphics
		private Trail trail;
		private const float invincibilityBlinkSpeed = 0.5f;

		//Survivability
		public int lives = 3;
		private float invincibilityTimer = 0.0f;
		private const float maxInvincibilityTimer = 5.0f;
		private const float maxFireDelay = 0.1f;

		//Firing
		private float fireDelay = 0;
		private const float bulletSpeed = 500.0f;
		private const float bulletSpread = 50.0f;

		//Movement
		private const float normalSpeed = 300.0f;
		private const float slowSpeed = 150.0f;
		private const float borderCollisionDistance = 24.0f;

		//Score
		public double score = 0;
		public int collectedPowerUps = 0;

		public Player(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
		{
			trail = new Trail(100, sb, ResourceManager.textures["Lenovo-DenovoMan"]);
			size = 5;
			ec = EntityManager.EntityClass.player;
		}

		public void Fire()
		{
			em.AddEntity(new Bullet(pos, sb, em, this, new Vector2(0, -bulletSpeed)));
			em.AddEntity(new Bullet(pos, sb, em, this, new Vector2(bulletSpread, -bulletSpeed)));
			em.AddEntity(new Bullet(pos, sb, em, this, new Vector2(-bulletSpread, -bulletSpeed)));
		}

		public override void Update(GameTime gt)
		{
			int SpriteSize = ResourceManager.textures["Lenovo-DenovoMan"].Height / 2;
			trail.AddSection(new Vector2(pos.X - SpriteSize, pos.Y - SpriteSize));
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

			if (pos.X + borderCollisionDistance > Global.playingFieldWidth) pos.X = Global.playingFieldWidth - borderCollisionDistance;
			if (pos.X - borderCollisionDistance < 0) pos.X = 0 + borderCollisionDistance;
			if (pos.Y + borderCollisionDistance > Global.screenHeight) pos.Y = Global.screenHeight - borderCollisionDistance;
			if (pos.Y - borderCollisionDistance < 0) pos.Y = 0 + borderCollisionDistance;

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
			if (invincibilityTimer <= 0 || (invincibilityTimer % invincibilityBlinkSpeed) >= invincibilityBlinkSpeed / 2)
			{
				int SpriteSize = ResourceManager.textures["Lenovo-DenovoMan"].Height / 2;
				sb.Draw(ResourceManager.textures["Lenovo-DenovoMan"], new Vector2(pos.X - SpriteSize, pos.Y - SpriteSize), Color.White);
				trail.Draw(gt);
				sb.Draw(ResourceManager.textures["Lenovo-DenovoMan"], new Vector2(pos.X - SpriteSize, pos.Y - SpriteSize), Color.White);
				MonoGame.Primitives2D.DrawCircle(sb, pos, size, 100, Color.Red, 5);
			}
		}

		public override void OnDamaged(Entity by, int damage)
		{
			if (invincibilityTimer <= 0)
			{
				for (int i = 0; i < 20; i++)
				{
					em.AddEntity(new Particle(pos, sb, em));
				}
				ResetPosition();
				lives--;
				invincibilityTimer = maxInvincibilityTimer;
			}
		}

		void ResetPosition()
		{
			pos = new Vector2(Global.playingFieldWidth / 2, Global.screenHeight - 50);
		}

		public void CollectPoint()
		{
			collectedPowerUps++;
			score += collectedPowerUps;
		}
	}
}

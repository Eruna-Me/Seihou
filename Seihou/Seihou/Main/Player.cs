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
		public int lives = 6;
		private float invincibilityTimer = 0.0f;
		private const float maxInvincibilityTimer = 5.0f;
		private const float maxFireDelay = 0.1f;

		//Firing
		public int power = 0;
		private const int powerStage1 = 3;
		private const int fullPower = 10;
		private float fireDelay = 0;
		private const float bulletSpeed = 500.0f;
		private const float bulletSpread = 30.0f;

		//Movement
		private const float normalSpeed = 300.0f;
		private const float slowSpeed = 150.0f;
		private const float borderCollisionDistance = 24.0f;

		//Score
		public double score = 0;
		public double graze = 0; // TODO: add graze
		public int grazeDistance = ResourceManager.textures["Lenovo-DenovoMan"].Height / 2;
		public int collectedPowerUps = 0;
		public const float pointBaseScore = 10000.0f;
		public const float pointCPUbonusScore = 100.0f;
		public const float powerBaseScore = 0.0f;
		public const float powerCPUbonusScore = 100.0f;
		public const float grazeScore = 10000.0f;

		public Player(SpriteBatch sb, EntityManager em) : base(new Vector2(0,0), sb, em)
		{
			texture = "Lenovo-DenovoMan";
			ResetPosition();
			trail = new Trail(100, sb, texture);
			size = 5;
			ec = EntityManager.EntityClass.player;
		}

		public override void Update(GameTime gt)
		{
			int SpriteSize = ResourceManager.textures[texture].Height / 2;
			trail.AddSection(pos);
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
				int SpriteSize = ResourceManager.textures[texture].Height / 2;
				sb.Draw(ResourceManager.textures[texture], pos - ResourceManager.Center(texture), Color.White);
				trail.Draw(gt);
				if (Global.drawCollisionBoxes) MonoGame.Primitives2D.DrawCircle(sb, pos, size, 100, Color.Red, 5);
				if (Global.drawCollisionBoxes) MonoGame.Primitives2D.DrawCircle(sb, pos, grazeDistance, 10, Color.White, 1);
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

        public void Fire()
        {
            em.AddEntity(new HomingBullet(pos, sb, em, this, new Vector2(bulletSpread, -bulletSpeed)));
            if (power >= powerStage1)
            {
                em.AddEntity(new HomingBullet(pos, sb, em, this, new Vector2(bulletSpread, -bulletSpeed)));
                em.AddEntity(new HomingBullet(pos, sb, em, this, new Vector2(-bulletSpread, -bulletSpeed)));
            }
            if (power >= fullPower)
            {
                em.AddEntity(new HomingBullet(pos, sb, em, this, new Vector2(-bulletSpread * 2, -bulletSpeed)));
                em.AddEntity(new HomingBullet(pos, sb, em, this, new Vector2(bulletSpread * 2, -bulletSpeed)));
            }
        }

        /*
		public void Fire()
		{
			em.AddEntity(new PlayerBullet(pos, sb, em, this, new Vector2(bulletSpread, -bulletSpeed)));
			if (power >= powerStage1)
			{
				em.AddEntity(new PlayerBullet(pos, sb, em, this, new Vector2(bulletSpread, -bulletSpeed)));
				em.AddEntity(new PlayerBullet(pos, sb, em, this, new Vector2(-bulletSpread, -bulletSpeed)));
			}
			if (power >= fullPower)
			{
				em.AddEntity(new PlayerBullet(pos, sb, em, this, new Vector2(-bulletSpread * 2, -bulletSpeed)));
				em.AddEntity(new PlayerBullet(pos, sb, em, this, new Vector2(bulletSpread * 2, -bulletSpeed)));
			}
		}
        */

        public void Graze(GameTime gt)
		{
			if (invincibilityTimer <= 0)
			{
				graze += (float)gt.ElapsedGameTime.TotalSeconds;
				score += grazeScore * (float)gt.ElapsedGameTime.TotalSeconds;
			}
		}

		void ResetPosition()
		{
			pos = new Vector2(Global.playingFieldWidth / 2, Global.screenHeight - 50);
		}

		public void CollectPoint()
		{
			collectedPowerUps++;
			score += pointBaseScore + collectedPowerUps * pointCPUbonusScore;
		}

		public void CollectPower()
		{
			power++;
			power = power > fullPower ? fullPower : power;
			collectedPowerUps++;
			score += powerBaseScore + collectedPowerUps * powerCPUbonusScore;
		}
	}
}

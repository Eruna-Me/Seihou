using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
    //Don't place outside mainstate.
	abstract class Player : Entity
	{
        //Other
        StateManager sm;
        protected State myState;

		//Graphics
		private Trail trail;
		private const float invincibilityBlinkSpeed = 0.5f;

		//Survivability
		public int lives; 
		private float invincibilityTimer = 0.0f;
		private const float maxInvincibilityTimer = 5.0f;

		//Firing
		public int power = 0;
		public int fullPower = 10;
		protected float fireDelay = 0;
		private const float maxFireDelay = 0.1f;
		public int bombs;
		protected float bombDelay = 0.0f;
		private const float maxBombDelay = 1.0f;

		//Movement
		private const float normalSpeed = 300.0f;
		private const float slowSpeed = 150.0f;
		private const float borderCollisionDistance = 24.0f;

		//Score
		public double score = 0;
		public double graze = 0;
		public int grazeDistance = (int)(ResourceManager.textures["Lenovo-DenovoMan"].Height / 2 * 1.5f);
		public int collectedPowerUps = 0;
		private const float pointBaseScore = 10000.0f;
		private const float pointCPUbonusScore = 100.0f;
		private const float powerBaseScore = 0.0f;
		private const float powerCPUbonusScore = 100.0f;
		private const float grazeScore = 10000.0f;

        public Player(SpriteBatch sb, EntityManager em,StateManager sm,State state) : base(new Vector2(0,0), sb, em)
		{
            this.sm = sm;
            this.myState = state;
			texture = "Lenovo-DenovoMan";
			ResetPosition();
			trail = new Trail(sb, texture,5,0.01f);
			size = 5;
			lives = Settings.GetInt("startingLives");
			bombs = Settings.GetInt("startingBombs");
			ec = EntityManager.EntityClass.player;
		}

		public override void Update(GameTime gt)
		{
			int SpriteSize = ResourceManager.textures[texture].Height / 2;
			trail.Update(pos,gt);
			KeyboardState kb = Keyboard.GetState();

			bool u = kb.IsKeyDown(Settings.GetKey("upKey"));
			bool r = kb.IsKeyDown(Settings.GetKey("rightKey"));
			bool d = kb.IsKeyDown(Settings.GetKey("downKey"));
			bool l = kb.IsKeyDown(Settings.GetKey("leftKey"));
			bool slowMode = kb.IsKeyDown(Settings.GetKey("slowKey"));

			bool s = kb.IsKeyDown(Settings.GetKey("shootKey"));
			bool b = kb.IsKeyDown(Settings.GetKey("bombKey"));

			//Movement
			speed.X = (Convert.ToInt32(r) - Convert.ToInt32(l)) * (slowMode ? slowSpeed : normalSpeed);
			speed.Y = (Convert.ToInt32(d) - Convert.ToInt32(u)) * (slowMode ? slowSpeed : normalSpeed);

			pos += speed * gt.Time();

			if (pos.X + borderCollisionDistance > Global.playingFieldWidth) pos.X = Global.playingFieldWidth - borderCollisionDistance;
			if (pos.X - borderCollisionDistance < 0) pos.X = 0 + borderCollisionDistance;
			if (pos.Y + borderCollisionDistance > Global.screenHeight) pos.Y = Global.screenHeight - borderCollisionDistance;
			if (pos.Y - borderCollisionDistance < 0) pos.Y = 0 + borderCollisionDistance;

			//Fire
			if (s && fireDelay <= 0)
			{
				Fire(slowMode);
				fireDelay = maxFireDelay;
			}
			if (b && bombDelay <= 0 && bombs > 0)
			{
				DropBomb();
				bombDelay = maxBombDelay;
				bombs--;
			}
			fireDelay -= 1 * gt.Time();
			bombDelay -= 1 * gt.Time();
			invincibilityTimer -= 1 * gt.Time();
		}

		public override void Draw(GameTime gt)
		{
			if (invincibilityTimer <= 0 || (invincibilityTimer % invincibilityBlinkSpeed) >= invincibilityBlinkSpeed / 2)
			{
				int SpriteSize = ResourceManager.textures[texture].Height / 2;
				trail.Draw(gt);
				sb.Draw(ResourceManager.textures[texture], pos - ResourceManager.Center(texture), Color.White);
				#if DRAWCOLBOX
				MonoGame.Primitives2D.DrawCircle(sb, pos, size, 100, Color.Red, 5);
				MonoGame.Primitives2D.DrawCircle(sb, pos, grazeDistance, 10, Color.White, 1);
				#endif
			}
		}

		public override void OnDamaged(Entity by, int damage)
		{
			if (invincibilityTimer <= 0)
			{
				SoundHelper.PlayRandom("ExplosionLong");

				for (int i = 0; i < 20; i++)
				{
					em.AddEntity(new Particle(pos, sb, em));
				}
				em.AddEntity(new MessageBox(pos + new Vector2(0, -50), sb, em, "KAPOFT", 5f, 0, 2f, "DefaultFont", 1f) { color = Color.Red });
				ResetPosition();
				invincibilityTimer = maxInvincibilityTimer;

				if (lives <= 0)
					((MainState)sm.GetCurrentState()).OnPlayerDeath();

				lives--;
				bombs = Settings.GetInt("startingBombs");
				collectedPowerUps = 0;
				//decrease powah?
			}
		}

        public abstract void Fire(bool slowMode);
		public abstract void DropBomb();

		public void Graze(GameTime gt)
		{
			if (invincibilityTimer <= 0)
			{
				graze += gt.Time();
				score += grazeScore * gt.Time();
			}
		}

		protected void ResetPosition()
		{
			pos = new Vector2(Global.playingFieldWidth / 2, Global.screenHeight - 50);
		}

		public void CollectPoint()
		{
			collectedPowerUps++;
			float scoreGain = pointBaseScore + collectedPowerUps * pointCPUbonusScore;
			score += scoreGain;

            em.AddEntity(new MessageBox(pos + new Vector2(0, -50), sb, em, scoreGain.ToString(), 2.5f, 0, 1, "DefaultFont", 1f) { color = Color.Blue });
        }

		public void CollectPower()
		{
			
			power++;
			power = power > fullPower ? fullPower : power;
			collectedPowerUps++;
			float scoreGain = powerBaseScore + collectedPowerUps * powerCPUbonusScore;
			score += scoreGain;

			em.AddEntity(new MessageBox(pos + new Vector2(0, -50), sb, em, scoreGain.ToString(), 2.5f, 0, 1, "DefaultFont", 1f) { color = Color.Red});
		}

		public static void SpreadShot(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, float bulletSpeed, string texture, float direction, float spread, int amount)
		{
			if (amount % 2 != 0)
				em.AddEntity(new PlayerBullet(pos, sb, em, owner, new Vector2((float)Math.Cos(direction) * bulletSpeed, (float)Math.Sin(direction) * bulletSpeed), texture));

			for (int i = amount / 2; i > 0; i--)
			{
				em.AddEntity(new PlayerBullet(pos, sb, em, owner, new Vector2((float)Math.Cos(direction - Math.PI / (spread / i)) * bulletSpeed, (float)Math.Sin(direction - Math.PI / (spread / i)) * bulletSpeed), texture));
				em.AddEntity(new PlayerBullet(pos, sb, em, owner, new Vector2((float)Math.Cos(direction + Math.PI / (spread / i)) * bulletSpeed, (float)Math.Sin(direction + Math.PI / (spread / i)) * bulletSpeed), texture));
			}
		}

		public void Continue()
		{
			lives = Settings.GetInt("startingLives");
			score = 0;
		}
	}
}

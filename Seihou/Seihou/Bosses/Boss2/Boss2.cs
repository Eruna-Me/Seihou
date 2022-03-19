using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	class Boss2 : Boss
    {
		private const float drops = 20;
        private const float borderWidth = 100;
        private const float hoverHeight = 100;
        private const float fallspeed = 150;
        private float moveSpeed = 125;
        private const float lowHpSpeed = 150;

        //Variable
        private bool startMoving = false;

		public Boss2(SpriteBatch sb, EntityManager em) : base(new Vector2(Global.Center.X, Global.spawnHeight), sb, em)
		{
			speed.Y = fallspeed;
			texture = "ManekiNeko";

			size = ResourceManager.textures[texture].Height / 2;
			hp = 500;

			highHp = hp;
			midHp = (int)(hp * 0.60f);
			lowHp = (int)(hp * 0.30f);
			
			//Patterns
			patterns[Stages.high].Add(new FlakBarrage(this, em, 3f / (float)(Settings.GetDifficulty() + 1),10));

			patterns[Stages.mid].Add(new LaunchHomingMissiles(this, em, 3f / (float)(Settings.GetDifficulty() + 1)));

			patterns[Stages.low].Add(new LaunchHomingMissiles(this, em, 3f / (float)(Settings.GetDifficulty() + 1)));
        }



        public override void Update(GameTime gt)
        {
            if (currentStage == Stages.low)
            {
                moveSpeed = lowHpSpeed;
            }

            if (pos.Y > hoverHeight && !startMoving)
            {
                speed.Y = 0;
                speed.X = moveSpeed;
                startMoving = true;
            }

            if (pos.X > Global.playingFieldWidth - borderWidth) speed.X = -moveSpeed;
            if (pos.X < borderWidth) speed.X = moveSpeed;

			if (wantsToLeave)
			{
				speed = new Vector2(-moveSpeed,-moveSpeed);

				if (pos.Y < -100)
				{
					em.RemoveEntity(this);
				}
			}

            pos += speed * gt.Time();

            base.Update(gt);
        }

        public override void OnDamaged(Entity by, int damage)
        {
            SoundHelper.PlayRandom("EnemyPain");
            hp--;

            if (hp <= 0)
            {
                SoundHelper.PlayRandom("ExplosionLong");

                for (int i = 0; i < explosionParticles; i++)
                {
                    em.AddEntity(new Particle(pos, sb, em));
                }

                for (int i = 0; i < drops; i++)
                {
                    Vector2 randomVec = new Vector2(Global.random.Next(-20, 21), Global.random.Next(-20, 21));

                    em.AddEntity(new Power(pos + randomVec, sb, em));

                    randomVec = new Vector2(Global.random.Next(-20, 21), Global.random.Next(-20, 21));

                    em.AddEntity(new Point(pos + randomVec, sb, em));
                }

                em.RemoveEntity(this);
            }
        }
    }
}

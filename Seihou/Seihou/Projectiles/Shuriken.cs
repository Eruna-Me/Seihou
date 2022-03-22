using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    internal class Shuriken : EnemyProjectile
    {
        private readonly Trail trail;

		private const float acceleration = 500f;
		private const float timeUntilPause = 0.2f;
		private const float pauseDuration = 0.1f;

		private bool isResumed = false;
		private float rotation = 0;
		private float resumeSpeed = 300;
		private float timer = 0;
		private Vector2 pauseSpeed;
		private Vector2 target;
			
        public Shuriken(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed,Vector2 pauseSpeed) : base(pos, sb, em, owner)
        {
            texture = "Shuriken";
			
			this.pauseSpeed = pauseSpeed;
			this.speed = speed + pauseSpeed;
            trail = new Trail(sb, texture,20,0.01f);
            size = ResourceManager.textures[texture].Height/2;
        }

        public override void Update(GameTime gt)
        {
			rotation += gt.Time();
			timer += gt.Time();
            trail.Update(pos,gt, rotation);

			if (timer < timeUntilPause)
			{//Prepare
			}
			else if (timer < pauseDuration + timeUntilPause)
			{//Pause
				speed = pauseSpeed;
			}
			else
			{//Attack
				if (!isResumed)
				{
					target = Global.player.pos - pos;
				}

				speed = Global.Normalize(target) * resumeSpeed;
				resumeSpeed += acceleration * gt.Time();
				isResumed = true;
			}

            base.Update(gt);
        }

        public override void Draw(GameTime gt)
        {
            trail.Draw(gt);
            sb.Draw(ResourceManager.textures[texture], pos, null, Color.White,(rotation * (float)Math.PI),ResourceManager.Center(texture), 1.0f, SpriteEffects.None, 0f);
		}

    }
}

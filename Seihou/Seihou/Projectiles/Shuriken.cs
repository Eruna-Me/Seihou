using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    class Shuriken : EnemyProjectile
    {
        Trail trail;
        float rotation = 0;
		const float acceleration = 1.005f;
		float timerUntilPause = 0;
		const float timeUntilPause = 0.2f;
		float timerPause = 0;
		const float timePause = 0.01f;
		bool pause = false;
		bool resume = false;
		const float resumeSpeed = 300;

		Vector2 pauseSpeed;
		
        public Shuriken(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed,Vector2 pauseSpeed) : base(pos, sb, em, owner)
        {
			this.pauseSpeed = pauseSpeed;
            texture = "Shuriken";
            trail = new Trail(sb, texture,20,0.01f);
			this.speed = speed + pauseSpeed;
            size = ResourceManager.textures[texture].Height/2;
        }

        public override void Update(GameTime gt)
        {
            rotation += gt.Time();
            trail.Update(pos,gt, rotation);

			if (pause)
			{
				speed = pauseSpeed;
				timerPause += gt.Time();
				if (timerPause >= timePause)
				{
					resume = true;
					pause = false;
					speed = Global.Normalize(Global.player.pos - pos) * resumeSpeed;
				}
			}
			else if (!resume)
			{
				speed *= acceleration;

				timerUntilPause += gt.Time();
				if (timerUntilPause >= timeUntilPause)
				{
					pause = true;
					speed = new Vector2(0, 0);
				}
			}
			else
			{
				speed *= acceleration;
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

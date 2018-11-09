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
    class Shuriken : EnemyProjectile
    {
        Trail trail;
        float rotation = 0;
		float acceleration = 1.005f;
		float timerUntilPause = 0;
		float timeUntilPause = 0.2f;
		float timerPause = 0;
		float timePause = 1.0f;
		bool pause = false;
		bool resume = false;
		float resumeSpeed = 300;

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
            rotation += (float)gt.ElapsedGameTime.TotalSeconds;
            trail.Update(pos,gt, rotation);

			if (pause)
			{
				speed = pauseSpeed;
				timerPause += (float)gt.ElapsedGameTime.TotalSeconds;
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

				timerUntilPause += (float)gt.ElapsedGameTime.TotalSeconds;
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

			if (pause)
				MonoGame.Primitives2D.DrawLine(sb, pos, Global.player.pos, Color.Red);
			if (resume)
				MonoGame.Primitives2D.DrawLine(sb, pos, pos + speed*1000, Color.Red);
		}

    }
}

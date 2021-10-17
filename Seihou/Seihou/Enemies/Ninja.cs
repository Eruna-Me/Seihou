using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Seihou
{
	class Ninja : Enemy
    {
        private const float fallSpeed = 40.0f;
        private readonly float maxFireDelay;
        private const float bulletSpeed = 200.0f;
        private float fireDelay = 0;
        private readonly int bullets;

        public Ninja([Position]Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
			bullets = 2 * (1+(int)Settings.GetDifficulty());
			maxFireDelay = (5 - (float)Settings.GetDifficulty()) * 0.2f;


            texture = "Ninja";
            ec = EntityManager.EntityClass.enemy;
            size = 40; 
            speed.Y = fallSpeed;
			hp = 10;
        }

        public override void Update(GameTime gt)
        {
            fireDelay -= gt.Time();

			if (fireDelay < 0 && Global.OnScreen(pos))
            {
                for (int i = 0; i < bullets; i++)
                {
                    float dir = ( (i + 1) * (float)((Math.PI * 2) / bullets));
                    em.AddEntity(new Shuriken(pos, sb, em, this, new Vector2((float)Math.Cos(dir),(float)Math.Sin(dir)) * bulletSpeed,speed));
                }
                fireDelay = maxFireDelay;
            }

            pos += speed * gt.Time();

            base.Update(gt);
        }
    }
}

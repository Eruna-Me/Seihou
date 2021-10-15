using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Seihou
{
	class Shooter : Enemy
    {
        private const float fallSpeed = 40.0f;
        private readonly float maxFireDelay;
        private const float bulletSpeed = 200.0f;
        private float fireDelay = 0;
        private readonly int bullets;


        public Shooter(Vector2 pos, SpriteBatch sb, EntityManager em,int bullets = 4,float maxFireDelay = 0.2f) : base(pos, sb, em)
        {
            texture = "MeanMan";
            this.maxFireDelay = maxFireDelay;
            this.bullets = bullets;
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
                    float dir = (i + 1) * (float)((Math.PI * 2) / bullets);
                    em.AddEntity(new EnemyBullet(pos, sb, em, this, new Vector2((float)Math.Cos(dir),(float)Math.Sin(dir)) * bulletSpeed));
                }
                fireDelay = maxFireDelay;
            }

            pos += speed * gt.Time();

            base.Update(gt);
        }
    }
}

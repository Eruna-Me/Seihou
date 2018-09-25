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
    class Shooter : Enemy
    {
        private const float fallSpeed = 40.0f;
        private readonly float maxFireDelay;
        private const float bulletSpeed = 200.0f;
        private float fireDelay = 0;
        private readonly int bullets;


        public Shooter(Vector2 pos, SpriteBatch sb, EntityManager em,int bullets = 4,float maxFireDelay = 0.2f) : base(pos, sb, em)
        {
            this.maxFireDelay = maxFireDelay;
            this.bullets = bullets;
            ec = EntityManager.EntityClass.enemy;
            size = 40; 
            speed.Y = fallSpeed;
        }

        public override void Update(GameTime gt)
        {
            fireDelay -= (float)gt.ElapsedGameTime.TotalSeconds;

            if (fireDelay < 0)
            {
                for (int i = 0; i < bullets; i++)
                {
                    float dir = (i + 1) * (float)((Math.PI * 2) / bullets);
                    em.AddEntity(new EnemyBullet(pos, sb, em, this, new Vector2((float)Math.Cos(dir),(float)Math.Sin(dir))* bulletSpeed));
                }
                fireDelay = maxFireDelay;
            }

            pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

            base.Update(gt);
        }

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.DrawCircle(sb, pos, size, 3, Color.Green, 3);
        }
    }
}

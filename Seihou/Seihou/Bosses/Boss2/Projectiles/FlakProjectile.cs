using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Seihou
{
    internal class FlakProjectile : EnemyProjectile
    {
        private float homingTime;
        private readonly int fragments;
        private readonly float fragmentSpeed;

        public FlakProjectile(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed, int fragments, float fragmentSpeed) : base(pos, sb, em, owner)
        {
            texture = "Dart2";
            this.speed = speed;
            this.fragments = fragments;
            this.fragmentSpeed = fragmentSpeed;
            homingTime = (float)Global.screenHeight / speed.Length() / (float)(Global.random.NextDouble() * 4 + 2);
        }

        public override void Update(GameTime gt)
        {
            pos += speed * gt.Time();

            if (homingTime < 0)
            {
                for (float i = 0; i <= Math.PI * 2 - ((float)(Math.PI * 2 / fragments) / 2); i += (float)(Math.PI * 2 / fragments))
                {
                    Vector2 dir = new Vector2((float)Math.Cos(i), (float)Math.Sin(i)) * fragmentSpeed;
                    em.AddEntity(new EnemyBullet(pos, sb, em, this, dir));
                }

                em.RemoveEntity(this);
            }
            else
            {
                homingTime -= gt.Time();
            }

            base.Update(gt);
        }
    }
}

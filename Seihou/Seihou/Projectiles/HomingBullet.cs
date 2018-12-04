using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    class HomingBullet : PlayerProjectile
    {
        protected Entity target;
        private readonly float bulletSpeed;
        private const float mooiBoogjeLevel = 5;
        private readonly float minimumBulletSpeed;
        private float homingTime;

        public HomingBullet(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner,Vector2 speed) : base(pos, sb, em, owner)
        {
            texture = "Dart2";
            this.speed = speed;
            minimumBulletSpeed = 1000;
            homingTime = Global.screenHeight / speed.Length();
            bulletSpeed = speed.Length();
        }

        public Entity LocateTarget()
        {
            Entity nearest = null;
            float distance = float.MaxValue;

            foreach (Entity e in em.GetEntities(EntityManager.EntityClass.enemy))
            {
                float dx = e.pos.X - pos.X;
                float dy = e.pos.Y - pos.Y;
                float dist = (float)Math.Sqrt((dx*dx) + (dy*dy));
                if (dist < distance)
                {
                    nearest = e;
                    distance = dist;
                }
            }
            return nearest;
        }

        public override void Update(GameTime gt)
        {
            if (speed.Length() < minimumBulletSpeed)
            {
                speed *= (minimumBulletSpeed / speed.Length());
            }

            pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

            if (pos.Y + Global.outOfScreenMargin < 0 || pos.Y > Global.screenHeight + Global.outOfScreenMargin || pos.X + Global.outOfScreenMargin < 0 || pos.X > Global.playingFieldWidth + Global.outOfScreenMargin)
            {
                em.RemoveEntity(this);
            }

            if (homingTime < 0)
            {
                return;
            }
            else
            {
                homingTime -= (float)gt.ElapsedGameTime.TotalSeconds;
            }

            if (target == null || target.hp < 1)
            {
                //Add timer here so it doesn't check 60 times per second if there are no enemies
                target = LocateTarget();
            }
            else
            {

                speed = (Global.Normalize(target.pos - pos) * bulletSpeed + speed * mooiBoogjeLevel) / (mooiBoogjeLevel+1);

               

                Entity c = em.Touching(this, EntityManager.EntityClass.enemy);

                if (c != null)
                {
                    if (c.hp > 0 && hp > 0)
                    {
                        hp--;
                        c.OnDamaged(owner, 1);
                        em.RemoveEntity(this);
                    }
                }
            }
        }

        public override void Draw(GameTime gt)
        {
            ResourceManager.DrawAngledTexture(sb, texture, pos, speed);
			#if DRAWCOLBOX
			MonoGame.Primitives2D.DrawCircle(sb, pos, size, 10, Color.White, 1);
			#endif
		}
	}
}

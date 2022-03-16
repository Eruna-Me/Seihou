using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    class HomingMissile : EnemyProjectile
    {
        private readonly float bulletSpeed;
        private const float mooiBoogjeLevel = 50;
        private readonly float minimumBulletSpeed;
        private float homingTime;

        public HomingMissile(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em, owner)
        {
            texture = "Dart2";
            this.speed = speed;
            minimumBulletSpeed = speed.Length() / 2;
            homingTime = Global.screenHeight / speed.Length();
            bulletSpeed = speed.Length();
        }

        public override void Update(GameTime gt)
        {
            if (speed.Length() < minimumBulletSpeed)
            {
                speed *= (minimumBulletSpeed / speed.Length());
            }

            pos += speed * gt.Time();

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
                homingTime -= gt.Time();
            }

            speed = (Global.Normalize(Global.player.pos - pos) * bulletSpeed + speed * mooiBoogjeLevel) / (mooiBoogjeLevel + 1);

            base.Update(gt);    
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

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
    }
}

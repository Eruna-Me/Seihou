using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    class HomingMissile : EnemyProjectile
    {
        private const float acceleration = 300;
        private readonly float minimumBulletSpeed;
        private readonly float maximumBulletSpeed;
        private float homingTime;

        public HomingMissile(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em, owner)
        {
            texture = "PinkHeart";
            this.speed = speed;
            minimumBulletSpeed = speed.Length() / 2;
            maximumBulletSpeed = speed.Length();
            homingTime = Global.screenHeight / speed.Length();
        }

        public override void Update(GameTime gt)
        {            

            if (homingTime > 0)
            {
                homingTime -= gt.Time();
                speed += (Global.Normalize(Global.player.pos - pos)) * acceleration * gt.Time();
            }

            if (speed.Length() < minimumBulletSpeed)
            {
                speed *= (minimumBulletSpeed / speed.Length());
            }

            if (speed.Length() > maximumBulletSpeed)
            {
                speed *= (maximumBulletSpeed / speed.Length());
            }

            pos += speed * gt.Time();

            base.Update(gt);    
        }
    }
}

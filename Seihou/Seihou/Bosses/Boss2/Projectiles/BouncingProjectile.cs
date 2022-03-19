using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    internal class BouncingProjectile : EnemyProjectile
    {
        private float bounces = 2;

        public BouncingProjectile(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em, owner)
        {
            texture = "Dart2";
            this.speed = speed;
        }

        public override void Update(GameTime gt)
        {			
            pos += speed * gt.Time();

            if (pos.Y < 0 || pos.Y > Global.screenHeight && bounces > 0)
            {
                speed.Y = -speed.Y;

                bounces--;
            }
            if (pos.X < 0 || pos.X > Global.playingFieldWidth && bounces > 0)
            {
                speed.X = -speed.X;

                bounces--;
            }

            base.Update(gt);
        }
    }
}

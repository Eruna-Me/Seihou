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
    class Faller : Enemy
    {
        //Byte arguments =

        private const float fallSpeed = 10.0f;
        private const float range = 100.0f;
        private const float swingSpeed = 5f;
        private const float fireRate = 0.05f;
        private float fireDelay = 0;
        private float startX;
        private readonly byte args;

        public Faller(Vector2 pos, SpriteBatch sb, EntityManager em,byte args) : base(pos, sb, em)
        {
            this.args = args;
            ec = EntityManager.EntityClass.enemy;
            startX = pos.X;
            size = 40; 
            speed.Y = fallSpeed;
        }

        public override void Update(GameTime gt)
        {
            pos.X = startX + (float)Math.Sin(gt.TotalGameTime.TotalSeconds * Math.PI * args) * range;
            pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

            fireDelay += (float)gt.ElapsedGameTime.TotalSeconds;

            if (fireDelay > fireRate)
            {
                em.AddEntity(new Bullet(pos, sb, em, this,new Vector2(0,200)));
                fireDelay = 0;
            }
        }

        public override void Damage(Entity by, int damage)
        {
            for (int i = 0; i < 4; i++)
            {
                em.AddEntity(new Particle(pos, sb, em));
            }
            em.RemoveEntity(this);
        }

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.DrawCircle(sb, pos, 10, size, Color.Orange, 3);
        }
    }
}

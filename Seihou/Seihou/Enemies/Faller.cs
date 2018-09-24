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
        private const float fallSpeed = 300.0f;
        private const float range = 100.0f;
        private const float swingSpeed = 5f;
        private const float maxFireDelay = 0.5f;
        private float fireDelay = 0.0f;
        private float startX;
        private float rotateTimer = 0.0f;
        private readonly bool direction;

        public Faller(Vector2 pos, SpriteBatch sb, EntityManager em,bool direction) : base(pos, sb, em)
        {
            this.direction = direction;
            ec = EntityManager.EntityClass.enemy;
            startX = pos.X;
            size = 40; 
            speed.Y = fallSpeed;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            rotateTimer += (float)gt.ElapsedGameTime.TotalSeconds;
            pos.X = startX + (float)Math.Sin(rotateTimer * (Math.PI + (Convert.ToDouble(direction)+1))) * range;
            pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

            fireDelay -= (float)gt.ElapsedGameTime.TotalSeconds;
            //Console.WriteLine("Fire delay is : " + fireDelay.ToString());
            if (fireDelay >= 0)
            {
                em.AddEntity(new Bullet(pos, sb, em, this,new Vector2(0,1000)));
                fireDelay = maxFireDelay;
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
            MonoGame.Primitives2D.DrawCircle(sb, pos, 10, 3, Color.Green, 3);
        }
    }
}

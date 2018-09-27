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
        private const float fallSpeed = 100.0f;
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

            if (fireDelay <= 0)
            {
                em.AddEntity(new Bullet(pos, sb, em, this,new Vector2(0,200)));
                fireDelay = maxFireDelay;
            }
        }
    }
}

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
    class Laserpen : Enemy
    {
        private const float fallSpeed = 40.0f;
        private readonly float maxFireDelay = 1f;
        private const float bulletSpeed = 300.0f;
        private float fireDelay = 0;
        private readonly int bullets;

        public Laserpen(Vector2 pos, SpriteBatch sb, EntityManager em, int bullets = 4) : base(pos, sb, em)
        {
            texture = "MeanMan";
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
                em.AddEntity(new EnergyBall(pos, sb, em, this, new Vector2(0, bulletSpeed)));
                fireDelay = maxFireDelay;
            }

            pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

            base.Update(gt);
        }
    }
}

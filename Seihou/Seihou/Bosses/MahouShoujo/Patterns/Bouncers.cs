using Microsoft.Xna.Framework;
using System;

namespace Seihou
{
    internal class Bouncers : Pattern
    {
        const float bulletSpeed = 125;

        readonly float spawnRate;
        readonly int amount;
        
        float spawnTimer = 0;

        public Bouncers(Boss daddy, EntityManager em, float spawnRate, int amount) : base(2, em, daddy)
        {
            this.spawnRate = spawnRate;
            this.amount = amount;
        }

        public override void Update(GameTime gt)
        {

            spawnTimer += gt.Time();

            if (spawnTimer > spawnRate)
            {
                spawnTimer = 0;

                for (float i = 0; i <= Math.PI * 2 - ((float)(Math.PI * 2 / amount) / 2); i += (float)(Math.PI * 2 / amount))
                {
                    var dir = new Vector2((float)Math.Cos(i), (float)Math.Sin(i)) * bulletSpeed;
                    em.AddEntity(new BouncingProjectile(owner.pos, owner.sb, em, owner, dir));
                }
            }

            base.Update(gt);
        }
    }
}


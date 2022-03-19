using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
    internal class FlakBarrage : Pattern
    {
        readonly float spawnRate;

        const float bulletSpeed = 150;
        const float spread = 150;
        const float fragmentSpeed = 250;

        int fragments;
        float spawnTimer = 0;

        public FlakBarrage(Boss owner, EntityManager em, float spawnRate, int fragments) : base(2, em, owner)
        {
            this.spawnRate = spawnRate;
            this.fragments = fragments;
        }

        public override void Update(GameTime gt)
        {
            spawnTimer += gt.Time();

            if (spawnTimer > spawnRate)
            {
                spawnTimer = 0;
                em.AddEntity(new FlakProjectile(owner.pos, owner.sb, em, owner, new Vector2(0, bulletSpeed), fragments, fragmentSpeed));
                em.AddEntity(new FlakProjectile(owner.pos, owner.sb, em, owner, new Vector2(spread, bulletSpeed), fragments, fragmentSpeed));
                em.AddEntity(new FlakProjectile(owner.pos, owner.sb, em, owner, new Vector2(-spread, bulletSpeed), fragments, fragmentSpeed));
                em.AddEntity(new FlakProjectile(owner.pos, owner.sb, em, owner, new Vector2(spread * 2, bulletSpeed), fragments, fragmentSpeed));
                em.AddEntity(new FlakProjectile(owner.pos, owner.sb, em, owner, new Vector2(-spread * 2, bulletSpeed), fragments, fragmentSpeed));
            }

            base.Update(gt);
        }
    }
}

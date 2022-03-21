using Microsoft.Xna.Framework;
using System;

namespace Seihou
{
	class Spray : Pattern
    {
		readonly float spawnRate;
        float spawnTimer = 0;
		const float spread = 20;
        const float bulletSpeed = 300;

        public Spray(Boss daddy, EntityManager em, float spawnRate) : base(2, em, daddy)
        {
            this.spawnRate = spawnRate;
        }

        public override void Update(GameTime gt)
        {
            spawnTimer += gt.Time();

            if (spawnTimer > spawnRate)
            {
                float Direction = Global.VtoD(Global.player.pos - owner.pos);

                spawnTimer = 0;
                em.AddEntity(new EnemyBullet(owner.pos,owner.sb, em, owner, new Vector2((float)Math.Cos(Direction - Math.PI / (spread / 2)) * bulletSpeed, (float)Math.Sin(Direction - Math.PI / (spread / 2)) * bulletSpeed), "Snowflake"));
                em.AddEntity(new EnemyBullet(owner.pos,owner.sb, em, owner, new Vector2((float)Math.Cos(Direction + Math.PI / (spread / 2)) * bulletSpeed, (float)Math.Sin(Direction + Math.PI / (spread / 2)) * bulletSpeed), "Snowflake"));

                em.AddEntity(new EnemyBullet(owner.pos,owner.sb, em, owner, Global.Normalize(Global.player.pos - owner.pos) * bulletSpeed, "Snowflake"));
            }

            base.Update(gt);
        }
    }
}


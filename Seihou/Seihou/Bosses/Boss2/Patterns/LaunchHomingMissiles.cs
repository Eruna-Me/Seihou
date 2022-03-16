using Microsoft.Xna.Framework;

namespace Seihou
{
	class LaunchHomingMissiles : Pattern
    {
		readonly float spawnRate;
        float spawnTimer = 0;
        const float bulletSpeed = 150;
		const float spread = 150;

        public LaunchHomingMissiles(Boss daddy,EntityManager em,float spawnRate) : base(2,em,daddy)
        {
            this.spawnRate = spawnRate;
        }

        public override void Update(GameTime gt)
        {
            spawnTimer += gt.Time();

            if (spawnTimer > spawnRate)
            {
                spawnTimer = 0;
                em.AddEntity(new HomingMissile(daddy.pos,daddy.sb, em, daddy, new Vector2(0,    bulletSpeed)));
                em.AddEntity(new HomingMissile(daddy.pos, daddy.sb, em, daddy, new Vector2(spread,  bulletSpeed)));
                em.AddEntity(new HomingMissile(daddy.pos, daddy.sb, em, daddy, new Vector2(-spread, bulletSpeed)));
                em.AddEntity(new HomingMissile(daddy.pos, daddy.sb, em, daddy, new Vector2(spread * 2, bulletSpeed)));
                em.AddEntity(new HomingMissile(daddy.pos, daddy.sb, em, daddy, new Vector2(-spread * 2, bulletSpeed)));
            }

            base.Update(gt);
        }
    }
}

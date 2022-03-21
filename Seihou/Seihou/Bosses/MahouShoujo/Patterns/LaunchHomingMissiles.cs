using Microsoft.Xna.Framework;

namespace Seihou
{
	class LaunchHomingMissiles : Pattern
    {
		readonly float spawnRate;
        float spawnTimer = 0;
        const float bulletSpeed = 150;
		const float spread = 150;

        public LaunchHomingMissiles(Boss owner,EntityManager em,float spawnRate) : base(2,em,owner)
        {
            this.spawnRate = spawnRate;
        }

        public override void Update(GameTime gt)
        {
            spawnTimer += gt.Time();

            if (spawnTimer > spawnRate)
            {
                spawnTimer = 0;
                em.AddEntity(new HomingMissile(owner.pos, owner.sb, em, owner, new Vector2(0, bulletSpeed)));
                em.AddEntity(new HomingMissile(owner.pos, owner.sb, em, owner, new Vector2(spread, bulletSpeed)));
                em.AddEntity(new HomingMissile(owner.pos, owner.sb, em, owner, new Vector2(-spread, bulletSpeed)));
                em.AddEntity(new HomingMissile(owner.pos, owner.sb, em, owner, new Vector2(spread * 2, bulletSpeed)));
                em.AddEntity(new HomingMissile(owner.pos, owner.sb, em, owner, new Vector2(-spread * 2, bulletSpeed)));
            }

            base.Update(gt);
        }
    }
}

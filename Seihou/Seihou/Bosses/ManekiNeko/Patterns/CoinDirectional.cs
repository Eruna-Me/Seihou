using Microsoft.Xna.Framework;

namespace Seihou
{
	class CoinDirectional : Pattern
    {
		readonly float spawnRate;
        float spawnTimer = 0;
        const float bulletSpeed = 300;

        public CoinDirectional(Boss daddy,EntityManager em,float spawnRate) : base(2,em,daddy)
        {
            this.spawnRate = spawnRate;
        }

        public override void Update(GameTime gt)
        {
            spawnTimer += gt.Time();

            if (spawnTimer > spawnRate)
            {
                em.AddEntity(new Coin(owner.pos, owner.sb, em, owner, Global.Normalize(Global.player.pos - owner.pos) * bulletSpeed));
                spawnTimer = 0;
            }

            base.Update(gt);
        }
    }
}

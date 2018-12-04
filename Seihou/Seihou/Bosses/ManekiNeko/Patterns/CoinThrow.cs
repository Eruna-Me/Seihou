using Microsoft.Xna.Framework;

namespace Seihou
{
	class CoinThrow : Pattern
    {
		readonly float spawnRate;
        float spawnTimer = 0;
        const float bulletSpeed = 250;
		const float spread = 50;

        public CoinThrow(Boss daddy,EntityManager em,float spawnRate) : base(2,em,daddy)
        {
            this.spawnRate = spawnRate;
        }

        public override void Update(GameTime gt)
        {
            spawnTimer += (float)gt.ElapsedGameTime.TotalSeconds;

            if (spawnTimer > spawnRate)
            {
                spawnTimer = 0;
                em.AddEntity(new Coin(daddy.pos,daddy.sb, em, daddy, new Vector2(0,    bulletSpeed)));
                em.AddEntity(new Coin(daddy.pos, daddy.sb, em, daddy, new Vector2(spread,  bulletSpeed)));
                em.AddEntity(new Coin(daddy.pos, daddy.sb, em, daddy, new Vector2(-spread, bulletSpeed)));
                em.AddEntity(new Coin(daddy.pos, daddy.sb, em, daddy, new Vector2(spread * 2, bulletSpeed)));
                em.AddEntity(new Coin(daddy.pos, daddy.sb, em, daddy, new Vector2(-spread * 2, bulletSpeed)));
            }

            base.Update(gt);
        }
    }
}

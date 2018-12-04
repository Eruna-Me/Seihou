using Microsoft.Xna.Framework;
using System;

namespace Seihou
{
	class CoinCircle : Pattern
    {
		readonly float spawnRate;
        float spawnTimer = 0;
        const float bulletSpeed = 300;
		readonly int amount;

        public CoinCircle(Boss daddy,EntityManager em,float spawnRate,int amount) : base(2,em,daddy)
        {
			this.amount = amount;
            this.spawnRate = spawnRate;
        }

        public override void Update(GameTime gt)
        {
            spawnTimer += (float)gt.ElapsedGameTime.TotalSeconds;

            if (spawnTimer > spawnRate)
            {
				spawnTimer = 0;

				for (float i = 0; i <= Math.PI*2-((float)(Math.PI*2/amount)/2); i += (float)(Math.PI*2/amount))
				{
					Vector2 dir = new Vector2((float)Math.Cos(i),(float)Math.Sin(i)) * bulletSpeed;
					em.AddEntity(new Coin(daddy.pos, daddy.sb, em, daddy, dir));
				}
            }

            base.Update(gt);
        }
    }
}

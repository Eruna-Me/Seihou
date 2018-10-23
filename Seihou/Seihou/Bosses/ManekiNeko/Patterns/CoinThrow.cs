using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Seihou
{
    class CoinThrow : Pattern
    {
        float spawnRate;
        float spawnTimer = 0;

        public CoinThrow(Boss daddy,EntityManager em,float spawnRate) : base(20,em,daddy)
        {
            this.spawnRate = spawnRate;
        }

        public override void Update(GameTime gt)
        {
            spawnTimer += (float)gt.ElapsedGameTime.TotalSeconds;

            if (spawnTimer > spawnRate)
            {
                spawnTimer = 0;
                em.AddEntity(new Coin(daddy.pos,daddy.sb, em, daddy, new Vector2(0,100)));
            }

            base.Update(gt);
        }
    }
}

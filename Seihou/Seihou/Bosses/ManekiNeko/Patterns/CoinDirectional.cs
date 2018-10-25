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
    class CoinDirectional : Pattern
    {
        float spawnRate;
        float spawnTimer = 0;
        const float bulletSpeed = 300;
        int amount;

        public CoinDirectional(Boss daddy,EntityManager em,float spawnRate,int amount) : base(2,em,daddy)
        {
            this.amount = amount;
            this.spawnRate = spawnRate;
        }

        public override void Update(GameTime gt)
        {
            spawnTimer += (float)gt.ElapsedGameTime.TotalSeconds;

            if (spawnTimer > spawnRate)
            {
                em.AddEntity(new Coin(daddy.pos, daddy.sb, em, daddy, Global.Normalize(Global.player.pos - daddy.pos) * bulletSpeed));
                spawnTimer = 0;
            }

            base.Update(gt);
        }
    }
}

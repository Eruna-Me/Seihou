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
    class CoinCircle : Pattern
    {
        float spawnRate;
        float spawnTimer = 0;
        const float bulletSpeed = 300;

        public CoinCircle(Boss daddy,EntityManager em,float spawnRate) : base(2,em,daddy)
        {
            this.spawnRate = spawnRate;
        }

        public override void Update(GameTime gt)
        {
            spawnTimer += (float)gt.ElapsedGameTime.TotalSeconds;

            if (spawnTimer > spawnRate)
            {
                spawnTimer = 0;
                Global.SpreadShot(daddy.pos, daddy.sb, em, daddy, bulletSpeed, "Coin", 0, 25, 51);
            }

            base.Update(gt);
        }
    }
}

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
        float timer = 0;

        public CoinThrow() : base(20)
        {

        }

        public override void Update(GameTime gt)
        {
            timer += (float)gt.ElapsedGameTime.TotalSeconds;



            base.Update(gt);
        }
    }
}

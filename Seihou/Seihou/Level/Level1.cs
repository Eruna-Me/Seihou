using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
    class Level1 : Level
    {
        public Level1(SpriteBatch sb, EntityManager em) : base(sb,em)
        {
            for (int i = 0; i < 500000; i++)
            {
                Spawn(new Shooter(Global.GetSpawn(i % 80 + 20), sb, em,10,10f),0.3f);
            }
        }
    }
}

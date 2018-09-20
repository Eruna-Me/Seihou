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
            for (int i = 0; i < 50000; i++)
            {
                Spawn(new Faller(Global.GetSpawn(100 - (i % 80) + 10), sb, em,false), 0.006f);
                Spawn(new Faller(Global.GetSpawn((i % 80) + 10), sb, em,true), 0.005f);
            }
        }
    }
}

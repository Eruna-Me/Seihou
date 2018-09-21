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
                Spawn(new Shooter(Global.GetSpawn(30), sb, em,902,0.5f), 20.0f);

                if (i % 10 == 0)
                {
                    Sleep(0.5f);
                }
            }
        }
    }
}

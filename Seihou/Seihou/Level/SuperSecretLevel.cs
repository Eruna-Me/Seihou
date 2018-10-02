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
    class SuperSecretLevel : Level
    {
        public SuperSecretLevel(SpriteBatch sb, EntityManager em) : base(sb, em)
        {

            Spawn(new MessageBox(Global.Center, sb, em, "Demo Level"));
            Sleep(3f);

            //PART 1
            for (int i = 0; i < 100; i++)
            {
                Spawn(new Faller(GetSpawn(5 + (i * 10)), sb, em, true));
                Spawn(new Faller(GetSpawn(95 - (i * 10)), sb, em, false));
                if (i % 5 == 0) Spawn(new JSF(GetSpawn(50), sb, em));

                Sleep(0.005f);
            }
            WaitUntilClear();
            Spawn(new MessageBox(Global.Center, sb, em, "REEEEEEEEEEEEEEE\nEEEEEEEEEEEEEE\nEEEEEEEEEEEEEEEE\nEEEEEEEEEEEEE"));
            Sleep(3f);

            Spawn(new JSF(GetSpawn(50), sb, em));

            for (int i = 0; i < 500; i++)
            {
                Spawn(new Shooter(GetSpawn(20 + (i % 80)), sb, em, 20, 5.0f));
                Spawn(new Shooter(GetSpawn(100 - (20 + (i % 80))), sb, em, 20, 5.0f));
                Sleep(0.095f);
            }
            Spawn(new JSF(GetSpawn(50), sb, em));
        }
    }
}

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

            Spawn(new MessageBox(Global.Center, sb, em, "Plz turn off \n fullscreen \n because it might \n crash, or \n save all your \n stuff first",5,5,5));
            Sleep(16.0f);

            Spawn(new MessageBox(Global.Center, sb, em, "Level 1"));


            //PART 1
            for (int i = 0; i < 10; i++)
            {
                Spawn(new Shooter(GetSpawn(5 + (i*5)), sb, em, 4, 5.0f));
                Spawn(new Shooter(GetSpawn(95 - (i*5)), sb, em, 4, 5.0f));

                Sleep(1f);
            }

            Sleep(2.5f);
            Spawn(new MessageBox(new Vector2(Global.playingFieldWidth/2,Global.screenHeight/2), sb, em, "Stage 2"));
            Sleep(2.5f);

            //PART 2
            for (int i = 0; i < 10; i++)
            {
                Spawn(new Faller(GetSpawn(5 + (i * 5)), sb, em,true));
                Spawn(new Faller(GetSpawn(95 - (i * 5)), sb, em,false));

                Sleep(1f);
            }


            Sleep(2.5f);
            Spawn(new MessageBox(new Vector2(Global.playingFieldWidth / 2, Global.screenHeight / 2), sb, em, "Stage 3"));
            Sleep(2.5f);


            //PART 3
            Spawn(new Shooter(GetSpawn(50), sb, em, 80, 3.0f),3.0f);
            Spawn(new Shooter(GetSpawn(10), sb, em, 80, 3.0f),3.0f);
            Spawn(new Shooter(GetSpawn(90), sb, em, 80, 3.0f),3.0f);


            Sleep(2.5f);
            Spawn(new MessageBox(new Vector2(Global.playingFieldWidth / 2, Global.screenHeight / 2), sb, em, "Stage 4"));
            Sleep(2.5f);


            //PART 4
            for (int i = 0; i < 35; i++)
            {
                Spawn(new Shooter(GetSpawn(55), sb, em, 8, 7.0f));
                Spawn(new Shooter(GetSpawn(45), sb, em, 8, 7.0f));
                Spawn(new JSF(GetSpawn(5), sb, em));
                Spawn(new JSF(GetSpawn(95), sb, em));

                if (i < 25)
                {
                    Sleep(2.0f);
                }
                else
                {
                    Sleep(1.0f);
                }
            }

            Sleep(2.5f);
            Spawn(new MessageBox(new Vector2(Global.playingFieldWidth / 2, Global.screenHeight / 2), sb, em, "Stage 5 (Final)"));
            Sleep(2.5f);

            //PART 5 FINAL
            for (int i = 0; i < 35; i++)
            {
                Spawn(new Shooter(GetSpawn(10), sb, em, 8, 1.0f));
                Spawn(new Shooter(GetSpawn(90), sb, em, 8, 1.0f));
                Spawn(new JSF(GetSpawn(5), sb, em));
                Spawn(new JSF(GetSpawn(95), sb, em));

                if (i % 5 == 0)
                {
                    Spawn(new Shooter(GetSpawn(50), sb, em, 50, 3.0f));
                }

                if (i < 25)
                {
                    Sleep(2.0f);
                }
            }

        }
    }
}

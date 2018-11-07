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
	class DemoLevel : Level
	{
		public DemoLevel (SpriteBatch sb, EntityManager em) : base(sb, em)
		{
			CloudManager.FillScreen(sb, em);
			
			Spawn(new MessageBox(Global.Center, sb, em, "Press Button") { waitForButtonPressOn = true});

			//part 0 rise of the unsamurai
			for (int i = 0; i < 5; i++)
			{
				Spawn(new Kitsune(GetSpawn(45), sb, em));
				Spawn(new Kitsune(GetSpawn(55), sb, em));

				Sleep(0.75f);
			}
			Sleep(3f);

			Spawn(new MessageBox(Global.Center, sb, em, "Demo Level"));
			Sleep(3f);

            Spawn(new ManekiNeko(sb, em));
		
			WaitUntilClear();

			//part 1 rise of the samurai
			for (int i = 0; i < 5; i++)
			{
				Spawn(new Samurai(GetSpawn(45), sb, em));
				Spawn(new Samurai(GetSpawn(55), sb, em));

				Sleep(0.75f);
			}
			Sleep(3f);

			for (int i = 0; i < 10; i++)
			{
				Spawn(new Samurai(GetSpawn(10), sb, em));
				Spawn(new Samurai(GetSpawn(20), sb, em));

				Sleep(0.5f);
			}
			Sleep(5f);

			for (int i = 0; i < 10; i++)
			{
				Spawn(new Samurai(GetSpawn(90), sb, em));
				Spawn(new Samurai(GetSpawn(80), sb, em));

				Sleep(0.5f);
			}
			Sleep(5f);

			//part 2 YukiOnna appears
			Spawn(new YukiOnna(GetSpawn(50), sb, em));

			Sleep(3f);

			//part 3 Combined arms warfare
			Spawn(new YukiOnna(GetSpawn(80), sb, em));
			for (int i = 0; i < 10; i++)
			{
				Spawn(new Samurai(GetSpawn(10), sb, em));
				Spawn(new Samurai(GetSpawn(20), sb, em));

				Sleep(0.5f);
			}
			Sleep(7.5f);

			Spawn(new YukiOnna(GetSpawn(20), sb, em));
			for (int i = 0; i < 10; i++)
			{
				Spawn(new Samurai(GetSpawn(90), sb, em));
				Spawn(new Samurai(GetSpawn(80), sb, em));

				Sleep(0.5f);
			}
			Sleep(7.5f);
		}
	}
}
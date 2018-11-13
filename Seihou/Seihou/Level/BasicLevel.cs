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
	class BasicLevel : Level
	{
		public BasicLevel (SpriteBatch sb, EntityManager em) : base(sb, em)
		{
			CloudManager.FillScreen(sb, em);

			Spawn(new MessageBox(Global.Center, sb, em, "          Level 1 \n" +
				"Use " + Settings.GetKey("upKey").ToString() + " " + Settings.GetKey("leftKey").ToString() + " " + Settings.GetKey("downKey").ToString() + " " + Settings.GetKey("rightKey").ToString() + " to move\n" +
				"press " + Settings.GetKey("shootKey").ToString() + " to fire \n" +
				"press " + Settings.GetKey("bombKey").ToString() + " to drop a bomb \n" +
				"press " + Settings.GetKey("slowKey").ToString() + " to move slower\n \n" +
				"press fire to continue") { waitForButtonPressOn = true});

			Sleep(1);

			WaitUntilClear();
			//Part 0 the last samurai

			Spawn(new Samurai(GetSpawn(50), sb, em));

			Sleep(2);

			//Part 0.5 the vengeful brothers

			Spawn(new Samurai(GetSpawn(40), sb, em));
			Spawn(new Samurai(GetSpawn(60), sb, em));

			Sleep(2);

			//Part 1 easy grind

			for (int i = 0; i < 5; i++)
			{
				Spawn(new Samurai(GetSpawn(10 + i * 5), sb, em));
				Sleep(0.75f);
			}

			Sleep(3);

			for (int i = 0; i < 5; i++)
			{
				Spawn(new Samurai(GetSpawn(90 - i * 5), sb, em));
				Sleep(0.75f);
			}

			Sleep(3);

			//part 2 medium grind

			for (int i = 0; i < 10; i++)
			{
				Spawn(new Samurai(GetSpawn(95 - i * 3), sb, em));
				Spawn(new Samurai(GetSpawn(5 + i * 3), sb, em));
				Sleep(1.5f);
			}

			Sleep(4);

			for (int i = 0; i < 10; i++)
			{
				Spawn(new Samurai(GetSpawn(50 - i * 3), sb, em));
				Spawn(new Samurai(GetSpawn(50 + i * 3), sb, em));
				Sleep(1f);
			}

			Sleep(4);

			//part 3 INTENSE grind

			for (int i = 0; i < 10; i++)
			{
				Spawn(new Samurai(GetSpawn(10), sb, em));
				Spawn(new Samurai(GetSpawn(20), sb, em));

				Sleep(0.6f);
			}
			Sleep(5f);

			for (int i = 0; i < 10; i++)
			{
				Spawn(new Samurai(GetSpawn(90), sb, em));
				Spawn(new Samurai(GetSpawn(80), sb, em));

				Sleep(0.6f);
			}
			Sleep(5f);

			//Part 4 Kitsune desu

			Spawn(new Kitsune(GetSpawn(50), sb, em, false));

			Sleep(2f);

			for (int i = 0; i < 8; i++)
			{
				Spawn(new Samurai(GetSpawn(55 - i * 5), sb, em));
				Spawn(new Samurai(GetSpawn(55 + i * 5), sb, em));
				Sleep(1.5f);
			}

			Sleep(5f);

			//Part 5 rain of bombs


			for (int i = 0; i < 50; i++)
			{
				Spawn(new Airmine(GetSpawn(Global.random.Next(0, 100)), sb, em));
				Sleep(0.25f);
			}

			Sleep(3f);

			//Part 6 operation snowstorm

			Spawn(new YukiOnna(GetSpawn(20), sb, em));
			Spawn(new YukiOnna(GetSpawn(80), sb, em));

			Sleep(5f);

			Spawn(new YukiOnna(GetSpawn(30), sb, em));

			Sleep(1f);

			Spawn(new YukiOnna(GetSpawn(50), sb, em));
			Spawn(new YukiOnna(GetSpawn(70), sb, em));

			//grand finals

			WaitUntilClear();

			Sleep(2f);

			Spawn(new ManekiNeko(sb, em));

			WaitUntilClear();

			//level 2

			Spawn(new MessageBox(Global.Center, sb, em, "        Level 2 \n" +
				"press fire to continue")
			{ waitForButtonPressOn = true });

			Sleep(1f);

			/*
			for (int i = 1; i < 10; i++)
			{
				Spawn(new Samurai(GetSpawn(50 - i*5), sb, em));
				Spawn(new Samurai(GetSpawn(50 + i*5), sb, em));
				Sleep(0.5f);
			}

			Sleep(4);

			for (int i = 1; i < 10; i++)
			{
				Spawn(new Samurai(GetSpawn(50 - i * 5), sb, em));
				Spawn(new Samurai(GetSpawn(50 + i * 5), sb, em));
				Sleep(0.5f);
			}
			Sleep(4);

			Spawn(new Shooter(GetSpawn(50), sb, em, 35, 5));
			WaitUntilClear();

			Spawn(new Shooter(GetSpawn(30), sb, em,90, 5));
			Spawn(new Shooter(GetSpawn(70), sb, em,90, 5));

			WaitUntilClear();

			Spawn(new Kitsune(GetSpawn(50), sb, em, false));

			WaitUntilClear();

			Spawn(new Airmine(GetSpawn(30), sb, em));
			Spawn(new Airmine(GetSpawn(70), sb, em));

			Sleep(0.5f);

			Spawn(new Samurai(GetSpawn(30), sb, em));
			Spawn(new Samurai(GetSpawn(70), sb, em));

			Spawn(new Samurai(GetSpawn(40), sb, em));
			Spawn(new Samurai(GetSpawn(60), sb, em));

			WaitUntilClear();

			Spawn(new Shooter(GetSpawn(50), sb, em));
			Sleep(0.5f);


			Spawn(new Shooter(GetSpawn(40), sb, em));
			Sleep(0.5f);

			Spawn(new Shooter(GetSpawn(60), sb, em));
			Sleep(0.5f);
			*/
		}
	}
}
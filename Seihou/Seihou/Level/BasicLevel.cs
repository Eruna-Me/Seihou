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
				"press " + Settings.GetKey("shootKey").ToString() + " to shoot \n" +
				"press " + Settings.GetKey("bombKey").ToString() + " to drop a bomb \n" +
				"press " + Settings.GetKey("slowKey").ToString() + " to move slower\n \n" +
				"press fire to continue") { waitForButtonPressOn = true});


			//Part1

			Sleep(1);


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

		}
	}
}
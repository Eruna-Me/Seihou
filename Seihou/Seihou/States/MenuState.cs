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
	class MenuState : State
	{

        StateManager sm;
		const int firstButtonHeight = 30;
        const int buttonsX = Global.screenWidth - 200;
		int y;
        Button[] buttons = new Button[5];

        void OnClickedStart()
        {

        }

        void OnClickedLevels()
        {

        }

        void OnClickedSettings()
        {

        }

        void OnClickedFinite()
        {

        }

        void  OnClickedExit()
        {
            sm.abort = true;
        }


		public MenuState (StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
            this.sm = sm;
            int s = 80; //Spacing
            int i = 0;

            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(300, 50), sb, OnClickedStart,  "Start");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(300, 50), sb, OnClickedLevels, "Levels");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(300, 50), sb, OnClickedSettings, "Infinite");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(300, 50), sb, OnClickedSettings, "Settings");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(300, 50), sb, OnClickedExit, "Exit");

		}

		public override void Draw(GameTime gt)
		{
            //MonoGame.Primitives2D.DrawLine(sb, new Vector2(Global.screenWidth / 2, Global.screenHeight), new Vector2(Global.screenWidth, 0),0,Color.Orange,100);
            foreach (Button b in buttons) b.Draw(gt);
		}

		public override void Update(GameTime gt)
		{
            foreach (Button b in buttons) b.Update(gt);
		}
	}
}

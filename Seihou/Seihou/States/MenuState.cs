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
		const int firstButtonHeight = 30;
        const int buttonsX = Global.screenWidth - 200;
        Button[] buttons = new Button[4];

        void OnClickedStart(object sender)
        {
            sm.ChangeState(new DifficultySelectionState(sm, cm, sb, gdm));
        }

        void OnClickedAbout(object sender)
        {
			sm.ChangeState(new AboutState(sm, cm, sb, gdm));
        }

        void OnClickedSettings(object sender)
        {
			sm.ChangeState(new SettingsState(sm, cm, sb, gdm));
        }

        void  OnClickedExit(object sender)
        {
            sm.abort = true;
        }


		public MenuState (StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
            int s = 80; //Spacing
            int i = 0;

            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(300, 50), sb, OnClickedStart,    "Start");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(300, 50), sb, OnClickedSettings, "Settings");
			buttons[i++] = new Button(new Vector2(buttonsX, firstButtonHeight + s * i), new Vector2(300, 50), sb,OnClickedAbout,    "About");
			buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(300, 50), sb, OnClickedExit,     "Exit");
		}

		public override void Draw(GameTime gt)
		{
            foreach (Button b in buttons) b.Draw(gt);
            sb.DrawString(ResourceManager.fonts["DefaultFont"], "O hi mark. you tearing me apart lisa", new Vector2(30,30), new Color(4, 4, 4));
            sb.Draw(ResourceManager.textures["Logo"], new Vector2(200, 200), Color.White);
        }

		public override void Update(GameTime gt)
		{
            foreach (Button b in buttons) b.Update(gt);
		}
	}
}

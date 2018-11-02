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
	class DifficultySelectionState : State
	{
		const int firstButtonHeight = 30;
        const int buttonsX = Global.screenWidth/2;
        Button[] buttons = new Button[5];

        void OnClickedEasy(object sender)
        {
			Settings.SetDifficulty(Settings.Difficulty.easy);
				sm.ChangeState(new MainState(sm, cm, sb, gdm));
		}

        void OnClickedMedium(object sender)
        {
			Settings.SetDifficulty(Settings.Difficulty.normal);
				sm.ChangeState(new MainState(sm, cm, sb, gdm));
		}

        void OnClickedHard(object sender)
        {
			Settings.SetDifficulty(Settings.Difficulty.hard);
				sm.ChangeState(new MainState(sm, cm, sb, gdm));
		}

        void OnClickedUsagi(object sender)
        {
			Settings.SetDifficulty(Settings.Difficulty.usagi);
				sm.ChangeState(new MainState(sm, cm, sb, gdm));
		}

        void  OnClickedExit(object sender)
        {
				sm.ChangeState(new MenuState(sm, cm, sb, gdm));
		}


		public DifficultySelectionState (StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
            int s = 100; //Spacing
            int i = 0;


            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(1000, 90), sb, OnClickedEasy,  "EASY \n    Eh? Easy modo? How lame! Only kids play on easy modo!!");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(1000, 90), sb, OnClickedMedium, "MEDIUM \n    Why don't you at least challenge as much as normal mode \n     once in your life.");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(1000, 90), sb, OnClickedHard, "HARD \n    The only proper difficulty setting.");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(1000, 90), sb, OnClickedUsagi, "UDONGE \n    Rabbits are scary animals.");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i + firstButtonHeight), new Vector2(300, 50), sb, OnClickedExit, "Back",Button.Align.center);

			buttons[0].textColor = Color.Green;
			buttons[1].textColor = Color.Yellow;
			buttons[2].textColor = Color.Red;
			buttons[3].textColor = Color.Purple;

		}

		public override void Draw(GameTime gt)
		{
			var f = ResourceManager.fonts["DefaultFont"];
			sb.DrawString(f, "Select difficulty", new Vector2(Global.screenWidth / 2, 50), Color.White, 0, f.MeasureString("Select difficulty")/2, 1, SpriteEffects.None, 0);
            foreach (Button b in buttons) b.Draw(gt);
        }

		public override void Update(GameTime gt)
		{
            foreach (Button b in buttons) b.Update(gt);
		}
	}
}

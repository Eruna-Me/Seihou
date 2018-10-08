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
        const int buttonsX = 100;
		float timer = 1;
        Button[] buttons = new Button[5];

        void OnClickedEasy()
        {
			Settings.difficulty = Settings.Difficulty.easy;
			if (0 > timer)
				sm.ChangeState(new TestState(sm, cm, sb, gdm));
		}

        void OnClickedMedium()
        {
			Settings.difficulty = Settings.Difficulty.normal;
			if (0 > timer)
				sm.ChangeState(new TestState(sm, cm, sb, gdm));
		}

        void OnClickedHard()
        {
			Settings.difficulty = Settings.Difficulty.hard;
			if (0 > timer)
				sm.ChangeState(new TestState(sm, cm, sb, gdm));
		}

        void OnClickedUsagi()
        {
			Settings.difficulty = Settings.Difficulty.usagi;
			if (0 > timer)
				sm.ChangeState(new TestState(sm, cm, sb, gdm));
		}

        void  OnClickedExit()
        {
			if (0 > timer)
				sm.ChangeState(new MenuState(sm, cm, sb, gdm));
		}


		public DifficultySelectionState (StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
            int s = 100; //Spacing
            int i = 0;

            
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(2000, 90), sb, OnClickedEasy,  "EASY \n    Eh? Easy modo? How lame! Only kids play on easy modo!!");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(2000, 90), sb, OnClickedMedium, "MEDIUM \n    Why don't you at least challenge as much as normal mode \n     once in your life.");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(2000, 90), sb, OnClickedHard, "HARD \n    The only proper difficulty setting.");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i), new Vector2(2000, 90), sb, OnClickedUsagi, "UDONGE \n    Rabbits are scary animals.");
            buttons[i++] = new Button(new Vector2(buttonsX,firstButtonHeight + s * i + firstButtonHeight), new Vector2(300, 50), sb, OnClickedExit, "Back");

		}

		public override void Draw(GameTime gt)
		{
            foreach (Button b in buttons) b.Draw(gt);
            //sb.DrawString(ResourceManager.fonts["DefaultFont"], "O hi mark. you tearing me apart lisa", new Vector2(30,30), new Color(4, 4, 4));
            //sb.Draw(ResourceManager.textures["Logo"], new Vector2(200, 200), Color.White);
        }

		public override void Update(GameTime gt)
		{
            foreach (Button b in buttons) b.Update(gt);
			timer -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;
		}
	}
}

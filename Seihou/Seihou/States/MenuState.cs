using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Seihou
{
	class MenuState : State
	{
		const int firstButtonHeight = 110;
        const int buttonsX = Global.screenWidth - 200;
        readonly Button[] buttons = new Button[5];

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

		void OnClickedHighscores(object sender)
		{
			sm.ChangeState(new HighscoreState(sm, cm, sb, gdm, 0, "none"));
		}

		void  OnClickedExit(object sender)
        {
            sm.abort = true;
        }

		public MenuState (StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
            const int spacing = 80;
            int i = 0;

            buttons[i] = new Button(new Vector2(buttonsX,firstButtonHeight + spacing * i), new Vector2(300, 50), sb, OnClickedStart,    "Start", i++);
			buttons[i] = new Button(new Vector2(buttonsX, firstButtonHeight + spacing * i), new Vector2(300, 50), sb, OnClickedHighscores, "Highscores", i++);
			buttons[i] = new Button(new Vector2(buttonsX,firstButtonHeight + spacing * i), new Vector2(300, 50), sb, OnClickedSettings, "Settings", i++);
			buttons[i] = new Button(new Vector2(buttonsX, firstButtonHeight + spacing * i), new Vector2(300, 50), sb,OnClickedAbout,    "About", i++);
			buttons[i] = new Button(new Vector2(buttonsX,firstButtonHeight + spacing * i), new Vector2(300, 50), sb, OnClickedExit,     "Exit", i++);
		}

		public override void Draw(GameTime gt)
		{
            foreach (Button b in buttons) b.Draw(gt);
            sb.DrawString(ResourceManager.fonts["DefaultFont"], "O hi mark. You're tearing me apart Lisa", new Vector2(30,30), new Color(4, 4, 4));
            sb.Draw(ResourceManager.textures["Logo"], new Vector2(200, 200), Color.White);
        }

		public override void Update(GameTime gt)
		{
			Cursor.Moved();
			Global.buttonCount = buttons.Length;
			Button.ButtonKeyControl(gt);
            foreach (Button b in buttons) b.Update(gt);
		}
	}
}

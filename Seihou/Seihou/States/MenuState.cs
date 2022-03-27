using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    internal class MenuState : State
	{
		private readonly FormHost host = new();
		const int firstButtonHeight = 110;
        private const int buttonsX = Global.screenWidth - 300;

		public MenuState (StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
			host.DefaultTabIndex = 0;

            host.AddControl(new Button(sb, OnClickedStart) { Text = "Start" });
			host.AddControl(new Button(sb, OnClickedHighscores) { Text = "Highscores" });
			host.AddControl(new Button(sb, OnClickedSettings) { Text = "Settings" });
			host.AddControl(new Button(sb, OnClickedAbout) { Text = "About" });
			host.AddControl(new Button(sb, OnClickedExit) { Text = "Exit", });

			const int spacing = 80;
			for (int i = 0; i < host.Controls.Count; i++)
            {
				var button = host.Controls[i] as Button;
				button.Align = TextAlign.Center;
				button.Gravity = TextGravity.Center;
				button.Size = new(200, 50);
				button.Position = new(buttonsX, firstButtonHeight + spacing * i);
				button.TabIndex = i;
			}
		}

		public override void Draw(GameTime gt)
		{
			sb.DrawString(ResourceManager.fonts["DefaultFont"], "O hi mark. You're tearing me apart Lisa", new Vector2(30, 30), new Color(4, 4, 4));
            sb.Draw(ResourceManager.textures["Logo"], new Vector2(200, 200), Color.White);
			host.Draw(gt);
        }

		public override void Update(GameTime gt)
		{
			host.Update(gt);
		}

		void OnClickedStart() => sm.ChangeState(new DifficultySelectionState(sm, cm, sb, gdm));
		void OnClickedAbout() => sm.ChangeState(new AboutState(sm, cm, sb, gdm));
		void OnClickedSettings() => sm.ChangeState(new SettingsState(sm, cm, sb, gdm));
		void OnClickedHighscores() => sm.ChangeState(new HighscoreState(sm, cm, sb, gdm, null));
		void OnClickedExit() => sm.abort = true;
	}
}

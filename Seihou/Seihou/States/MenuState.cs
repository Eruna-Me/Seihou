using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Seihou
{
    internal class MenuState : State
	{
		private const int contributorsX = 60;
		private const int contributorsY = Global.screenHeight - 120;
		private const int contributorsSpacing = 50;

		private readonly FormHost host = new();
		private const int firstButtonHeight = 110;
        private const int buttonsX = Global.screenWidth - 300;

		public MenuState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
		{
			host.DefaultTabIndex = 0;

			host.AddControl(new Button(sb, OnClickedStart) { Text = "Start" });
			host.AddControl(new Button(sb, OnClickedHighscores) { Text = "Highscores" });
			host.AddControl(new Button(sb, OnClickedSettings) { Text = "Settings" });
			host.AddControl(new Button(sb, OnClickedExit) { Text = "Exit", });

			int tabIndex = 0;
			const int spacing = 80;
			for (int i = 0; i < host.Controls.Count; i++)
			{
				var button = host.Controls[i] as Button;
				button.Align = TextAlign.Center;
				button.Gravity = TextGravity.Center;
				button.Size = new(200, 50);
				button.Position = new(buttonsX, firstButtonHeight + spacing * i);
				button.TabIndex = tabIndex++;
			}

			host.AddControl(new Button(sb, OnClickedErunaMe)
			{
				Position = new Vector2(contributorsX, contributorsY),
				Size = new Vector2(500, contributorsSpacing),
				Text = "Hidde Bartelink - Eruna.Me",
				Align = TextAlign.Left,
				TabIndex = tabIndex++,
			});

			host.AddControl(new Button(sb, OnClickedDennis)
			{
				Position = new Vector2(contributorsX, contributorsY + contributorsSpacing),
				Size = new Vector2(500, contributorsSpacing),
				Text = "Den-nis - github.com/den-nis",
				Align = TextAlign.Left,
				TabIndex = tabIndex++,
			});
		}

		public override void Draw(GameTime gt)
		{
            sb.Draw(ResourceManager.textures["Logo"], new Vector2(200, 200), Color.White);
			sb.DrawString(ResourceManager.fonts["DefaultFont"], "Made by", new Vector2(contributorsX, contributorsY - contributorsSpacing), new Color(100, 100, 100));
			host.Draw(gt);
        }

		public override void Update(GameTime gt)
		{
			host.Update(gt);
		}

		void OnClickedErunaMe() => OpenUrl("https://Eruna.Me/");

		void OnClickedDennis() => OpenUrl("https://www.github.com/den-nis/");

		static void OpenUrl(string url)
		{
			try
			{// hack because of this: https://github.com/dotnet/corefx/issues/10361
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					url = url.Replace("&", "^&");
					Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				{
					Process.Start("xdg-open", url);
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				{
					Process.Start("open", url);
				}
			}
			catch
			{
				// D:
			}
		}
	
		void OnClickedStart() => sm.ChangeState(new DifficultySelectionState(sm, cm, sb, gdm));
		void OnClickedSettings() => sm.ChangeState(new SettingsState(sm, cm, sb, gdm));
		void OnClickedHighscores() => sm.ChangeState(new HighscoreState(sm, cm, sb, gdm, null));
		void OnClickedExit() => sm.abort = true;
	}
}

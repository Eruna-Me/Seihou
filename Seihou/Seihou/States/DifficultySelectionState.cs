using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace Seihou
{
	internal class DifficultySelectionState : State
	{
		const int firstButtonHeight = 130;
        const int buttonsX = 150;

		private readonly FormHost host = new();

		public DifficultySelectionState (StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
			int spacing = 100;


			host.DefaultTabIndex = 1;

			var texts = new Dictionary<Settings.Difficulty, string>
			{
				//I did not make up these difficulty texts. !
				[Settings.Difficulty.easy] = "Eh? Easy modo? How lame! Only kids play on easy modo!!",
				[Settings.Difficulty.normal] = "Why don't you at least challenge as much as normal mode \n     once in your life.",
				[Settings.Difficulty.hard] = "The only proper difficulty setting.",
				[Settings.Difficulty.usagi] = "Rabbits are scary animals.",
			};

			foreach(var difficulty in Enum.GetValues<Settings.Difficulty>())
            {
				host.AddControl(new Button(sb, () => StartGame(difficulty), Global.GetDifficultyColor(difficulty))
				{ 
					Text = $"{Global.GetDifficultyDisplayName(difficulty)} \n    {texts[difficulty]}" 
				});
			}

			int tabIndex = 0;

			for (int i = 0; i < host.Controls.Count; i++)
            {
				var button = host.Controls[i] as Button;
				button.Align = TextAlign.Left;
				button.Position = new Vector2(buttonsX, firstButtonHeight + spacing * i);
				button.Size = new Vector2(1000, 90);
				button.TabIndex = tabIndex++;
				button.TextColorTabIndex = button.TextColor;
				button.BackgroundColorTabIndex = new Color(40, 40, 0, 50);
			}

			host.AddControl(new Button(sb, Exit)
			{
				Text = "Back",
				Position = new Vector2(50, 50),
				Size = new Vector2(100, 50),
				TabIndex = tabIndex++,
			});
		}

		public override void Draw(GameTime gt)
		{
			var f = ResourceManager.fonts["DefaultFont"];
			sb.DrawString(f, "Select difficulty", new Vector2(Global.screenWidth / 2, 50), Color.White, 0, f.MeasureString("Select difficulty")/2, 1, SpriteEffects.None, 0);
			host.Draw(gt);
        }

		public override void Update(GameTime gt)
		{
			host.Update(gt);
		}

		void Exit()
		{
			sm.ChangeState(new MenuState(sm, cm, sb, gdm));
		}

		void StartGame(Settings.Difficulty difficulty)
		{
			Settings.SetDifficulty(difficulty);
			sm.ChangeState(new MainState(sm, cm, sb, gdm));
		}
	}
}

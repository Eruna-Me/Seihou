using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using static Seihou.Settings;

namespace Seihou
{
	class HighscoreState : State
    {
		public record PlayData
		{
			public Difficulty Difficulty { get; init; }
			public double Score { get; init; }
		}

		private Difficulty _viewDifficulty = Difficulty.easy;
		private readonly FormHost host = new();

		private bool HasPlayed => _playData != null;
		private readonly PlayData _playData = null;

        private readonly OnScreenKeyboard _usernameTextbox;
		private readonly ScoreDisplay _scoreDisplay;

        public HighscoreState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm, PlayData playData) : base(sm, cm, sb, gdm)
        {
			int tabIndex = 0;

			_playData = playData;			
			_scoreDisplay = new ScoreDisplay(new Vector2(500, 100), new Vector2(650, 395), sb);
			_scoreDisplay.SetModeFilter(_viewDifficulty);

			var difficulties = Enum.GetValues(typeof(Difficulty));
			for (int i = 0; i < difficulties.Length; i++)
			{
				var difficulty = ((Difficulty)i);
				var button = new Button(sb, () => FilterButtonPressed(difficulty))
				{
					Position = new Vector2(200, 200 + i * 50),
					Size = new Vector2(200, 50),
					TabIndex = tabIndex++,
					Text = difficulty.ToString()
				};

				host.AddControl(button);
			}

			host.AddControl(new Button(sb, GoHome)
			{
				Text = "Back",
				Position = new Vector2(80, 640),
				Size = new Vector2(100, 25),
				TabIndex = tabIndex++
			});

			host.AddControl(_scoreDisplay);

			if (HasPlayed)
			{
				_usernameTextbox = new OnScreenKeyboard(new Vector2(100, 550), sb, OnSubmit);
				host.AddControl(_usernameTextbox);
			}
		}

		private void FilterButtonPressed(Difficulty difficulty)
		{
			_viewDifficulty = difficulty;
			_scoreDisplay.SetModeFilter(difficulty);
		}

        public override void Draw(GameTime gt)
        {
			if (HasPlayed)
			{
				sb.DrawString(ResourceManager.fonts["DefaultFont"], $"Your score: {_playData.Score}\nOn {_playData.Difficulty}", new Vector2(10, 10), Color.White);
				sb.DrawString(ResourceManager.fonts["DefaultFont"], $"Highscores: {_viewDifficulty}", new Vector2(500, 50), Color.White);
			}

			host.Draw(gt);
        }

		public override void Update(GameTime gt) => host.Update(gt);
		
		public void GoHome() => sm.ChangeState(new MenuState(sm, cm, sb, gdm));
		
		public void OnSubmit()
		{
			_scoreDisplay.SubmitScore(new ScoreRecord
			{
				Score = _playData.Score,
				Name = _usernameTextbox.text,
				Difficulty = _playData.Difficulty,
			});
		}

        public override void OnStart()
        {
			_scoreDisplay.Load();
        }
        
        public override void OnExit()
        {
        }
    }  
}

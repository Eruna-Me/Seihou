using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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

        private readonly TextField _textField;
		private readonly ScoreDisplay _scoreDisplay;

		private Dictionary<Difficulty, Button> _buttonLookup = new Dictionary<Difficulty, Button>();

        public HighscoreState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm, PlayData playData) : base(sm, cm, sb, gdm)
        {
			int tabIndex = 0;

			_playData = playData;			
			_scoreDisplay = new ScoreDisplay(new Vector2(500, 100), new Vector2(650, 395), sb);

			var difficulties = Enum.GetValues(typeof(Difficulty));
			for (int i = 0; i < difficulties.Length; i++)
			{
				var difficulty = ((Difficulty)i);
				var button = new Button(sb, () => SelectDifficulty(difficulty))
				{
					Position = new Vector2(200, 200 + i * 50),
					Size = new Vector2(200, 50),
					TabIndex = tabIndex++,
					Text = Global.GetDifficultyDisplayName(difficulty)
				};

				_buttonLookup.Add(difficulty, button);
				host.AddControl(button);
			}


			host.AddControl(_scoreDisplay);

			if (HasPlayed)
			{
				_textField = new TextField(sb, (b) => host.BlockUserInput = b)
				{
					Size = new Vector2(650, 50),
					Position = new Vector2(500, 500),
					Text = "Enter your name here and press enter",
					TabIndex = tabIndex++,
				};

				_textField.OnSubmit += (_,_) => OnSubmit();
				_textField.SetSimpleColorBackground(Color.DarkBlue);
				host.AddControl(_textField);
			}

			host.AddControl(new Button(sb, GoHome)
			{
				Text = "Back",
				Position = new Vector2(80, 640),
				Size = new Vector2(100, 25),
				TabIndex = tabIndex++,
			});

			SelectDifficulty(_playData?.Difficulty ?? Difficulty.normal);
		}

		private void SelectDifficulty(Difficulty difficulty)
		{
			foreach(var item in _buttonLookup)
            {
				item.Value.SetSimpleColorText(Global.GetDifficultyColor(item.Key));
				item.Value.Text = "  " + Global.GetDifficultyDisplayName(item.Key);
            }

			_buttonLookup[difficulty].SetSimpleColorText(Color.Orange);
			_buttonLookup[difficulty].Text = $"> {Global.GetDifficultyDisplayName(difficulty)}";

			_viewDifficulty = difficulty;
			_scoreDisplay.SetModeFilter(difficulty);
		}

        public override void Draw(GameTime gt)
        {
			if (HasPlayed)
			{
				sb.DrawString(ResourceManager.fonts["DefaultFont"], $"Your score: {_playData.Score}\nOn {Global.GetDifficultyDisplayName(_playData.Difficulty)}", new Vector2(10, 10), Color.White);
			}

			sb.DrawString(ResourceManager.fonts["DefaultFont"], $"Highscores: {Global.GetDifficultyDisplayName(_viewDifficulty)}", new Vector2(500, 50), Color.White);
			host.Draw(gt);
        }

		public override void Update(GameTime gt) => host.Update(gt);
		
		public void GoHome() => sm.ChangeState(new MenuState(sm, cm, sb, gdm));
		
		public void OnSubmit()
		{
			_scoreDisplay.SubmitScore(new ScoreRecord
			{
				Score = _playData.Score,
				Name = _textField.Text,
				Difficulty = _playData.Difficulty,
				Id = Guid.NewGuid(),
			});

			SelectDifficulty(_playData.Difficulty);
			_textField.Enabled = false;
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

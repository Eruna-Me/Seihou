using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using static Seihou.Settings;
using System.Linq;

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
		private readonly List<Control> _controls = new();

		private bool HasPlayed => _playData != null;
		private readonly PlayData _playData = null;

        private readonly Textbox _usernameTextbox;
		private readonly ScoreDisplay _scoreDisplay;

        public HighscoreState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm, PlayData playData) : base(sm, cm, sb, gdm)
        {
			int FormIndex = 0;

			_playData = playData;			
			_scoreDisplay = new ScoreDisplay(new Vector2(500, 100), new Vector2(650, 395), sb);
			_scoreDisplay.SetModeFilter(_viewDifficulty);

			var difficulties = Enum.GetValues(typeof(Difficulty));
			for (int i = 0; i < difficulties.Length; i++)
			{
				var position = new Vector2(200, 200 + i * 50);
				var size = new Vector2(200, 50);
				var difficulty = (Difficulty)i;

				var button = new Button(position, size, sb, (_) => FilterButtonPressed(difficulty), difficulty.ToString(), FormIndex++, Button.Align.center);

				_controls.Add(button);
			}

			_controls.Add(new Button(new Vector2(80, 690), new Vector2(100, 25), sb, GoHome, "Back", FormIndex++, Button.Align.center));
			_controls.Add(_scoreDisplay);

			if (HasPlayed)
			{
				_usernameTextbox = new Textbox(new Vector2(100, 550), sb, OnSubmit);
				_controls.Add(_usernameTextbox);
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

			foreach (var control in _controls)
			{
				control.Draw(gt);
			}
        }
        
        public override void Update(GameTime gt)
        {
			Cursor.Moved();
			Global.buttonCount = _controls.Count(c => c is Button);
			Button.ButtonKeyControl(gt);

			foreach (var control in _controls)
			{
				control.Update(gt);
			}
		}

		public void GoHome(object sender)
		{
			sm.ChangeState(new MenuState(sm, cm, sb, gdm));
		}

		public void OnSubmit(object sender)
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

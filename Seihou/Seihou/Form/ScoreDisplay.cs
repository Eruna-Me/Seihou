using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Seihou
{
	class ScoreDisplay : Control
    {
		public IReadOnlyList<string> ScoreText { get; private set; }

		private readonly ScoreRepository _scoreRepository = new();

		private const string FONT = "DefaultFont";
		private const int MAX_RECORDS_DISPLAY = 10;
        private const int Y_SPACING = 40;

		private Settings.Difficulty _filter = Settings.Difficulty.easy;
		private Vector2 _position;
		private Vector2 _size;

		public ScoreDisplay(Vector2 pos, Vector2 size, SpriteBatch sb) : base(sb)
        {
			_size = size;
			_position = pos;
		}

		public void Load()
		{
			_scoreRepository.Load();
			UpdateScoreText();
		}

		public void SubmitScore(ScoreRecord record)
		{
			_scoreRepository.Write(record);
			_scoreRepository.Save();
			UpdateScoreText();
		}

        public override void Draw(GameTime gt)
        {
			MonoGame.Primitives2D.FillRectangle(sb, _position, _size, background, 0);

			for (int i = 0; i < ScoreText.Count; i++)
			{
				sb.DrawString(ResourceManager.fonts[FONT], ScoreText[i] ?? "Empty", new Vector2(_position.X, _position.Y + (i * Y_SPACING)), textColor);
			}			
        }

		public void SetModeFilter(Settings.Difficulty difficulty)
		{
			_filter = difficulty;
			UpdateScoreText();
		}

		private void UpdateScoreText()
		{
			List<string> scoreText = new();
			var records = _scoreRepository.GetScoreRecords(_filter)
				.Take(MAX_RECORDS_DISPLAY)
				.ToArray();

			var numberLength = MAX_RECORDS_DISPLAY.ToString().Length + 2;

			for (int i = 0; i < records.Length; i++)
			{
				var name = records[i].Name.ToString();
				var score = records[i].Score.ToString();

				scoreText.Add(
					Clip($"#{i + 1}", numberLength) + $"{Clip(name, 20)} | {Clip(score, 20)}"
				);
			}

			ScoreText = scoreText;
		}

		private static string Clip(string str, int length)
		{
			if (str.Length > length)
				return str.Substring(0, length);

			if (str.Length < length)
				return str + new string(' ', length - str.Length);

			return str;
		}

		public override void Update(GameTime gt)
		{

		}
	}
}

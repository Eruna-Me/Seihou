using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
		private Dictionary<Settings.Difficulty, float> _scroll = new();
		private float _lastScroll;

		public ScoreDisplay(Vector2 pos, Vector2 size, SpriteBatch sb) : base(sb)
        {
			_size = size;
			_position = pos;
			_lastScroll = Mouse.GetState().ScrollWheelValue;
			foreach (var d in Enum.GetValues<Settings.Difficulty>())
            {
				_scroll[d] = 0;
            }
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
			_filter = record.Difficulty;

			AutoScrollTo(record.Id);
			UpdateScoreText();
		}

        public override void Draw(GameTime gt)
        {
			MonoGame.Primitives2D.FillRectangle(sb, _position, _size, new Color(20,20,50), 0);

			for (int i = 0; i < ScoreText.Count; i++)
			{
				sb.DrawString(ResourceManager.fonts[FONT], ScoreText[i] ?? "Empty", new Vector2(_position.X, _position.Y + (i * Y_SPACING)), Color.White);
			}			
        }

		public void SetModeFilter(Settings.Difficulty difficulty)
		{
			_filter = difficulty;
			UpdateScoreText();
		}

		private void AutoScrollTo(Guid id)
        {
			//TODO: make this less hacky
			var offset = _scoreRepository.GetScoreRecords(_filter)
				.TakeWhile(x => x.Id != id)
				.Count();

			_scroll[_filter] = offset;
		}

		private void UpdateScoreText()
		{
			List<string> scoreText = new();
			int offset = (int)Math.Round(_scroll[_filter]);

			var records = _scoreRepository.GetScoreRecords(_filter)
				.Skip(offset)
				.Take(MAX_RECORDS_DISPLAY)
				.ToArray();

			var numberLength = MAX_RECORDS_DISPLAY.ToString().Length + 2;

			for (int i = 0; i < MAX_RECORDS_DISPLAY; i++)
			{
				string number = Clip($"#{offset + i + 1}", numberLength);

				if (i < records.Length)
				{
					var name = records[i].Name?.ToString() ?? string.Empty;
					var score = records[i].Score.ToString();

					scoreText.Add(number + $"{Clip(name, 20)} | {Clip(score, 20)}");
				}
				else
                {
					scoreText.Add(number);
                }
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
			var mState = Mouse.GetState();
			if (Intersects(mState.Position.ToVector2()))
            {
				_scroll[_filter] += (_lastScroll - mState.ScrollWheelValue) * 0.01f;
				_scroll[_filter] = _scroll[_filter] < 0 ? 0 : _scroll[_filter];
				
				UpdateScoreText();
            }

			_lastScroll = mState.ScrollWheelValue;
		}

		private bool Intersects(Vector2 point)
		{
			return
				(point.X > _position.X) &&
				(point.X < _position.X + _size.X) &&
				(point.Y > _position.Y) &&
				(point.Y < _position.Y + _size.Y);
		}
	}
}

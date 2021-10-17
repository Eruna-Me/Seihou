using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou.Level.Logic
{
	class LogicDialog : LogicEntity
	{
		private readonly LevelManager _levelManager;
		private readonly bool _pause;
		private readonly string _text;
		private readonly Color _color;
		private readonly string _font;
		private readonly string[] _lines;
		private readonly float _totalTime;

		public LogicDialog(EntityManager em, SpriteBatch sb, LevelManager levelManager,
			[Param("Time")] float time,
			[Param("Pause")] bool pause,
			[Param("Text")] string text,
			[Param("Color")] Color color,
			[Param("Font")] string font
			) : base(em, sb, time)
		{
			ec = EntityManager.EntityClass.ui;
			_levelManager = levelManager;
			_pause = pause;
			_text = text;
			_color = color;
			_font = font;
			_lines = _text.Split('\n');
			_totalTime = time;
		}

		public override void Draw(GameTime gt)
		{
			var font = ResourceManager.fonts[_font];

			var lineIndex = (int)(_lines.Length * (1 - Time / _totalTime));
			var line = _lines[lineIndex];
			sb.DrawString(font, line, Global.Center - font.MeasureString(line) / 2, _color);
		}

		public override void Update(GameTime gt)
		{
			base.Update(gt);
		}

		protected override void OnTimerFinished()
		{
			if (_pause)
			{
				_levelManager.Unpause(this);
			}
		}

		public override void OnSpawn()
		{
			if (_pause)
			{
				_levelManager.Pause(this);
			}
		}
	}
}
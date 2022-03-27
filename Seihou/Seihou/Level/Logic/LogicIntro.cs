using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou.Level.Logic
{
    internal class LogicIntro : LogicEntity
    {
        private readonly TextLabel _label;
		private readonly SpriteFont _font;
		private readonly Color _color;
        private readonly float _fadeInTime;
        private readonly float _holdTime;
        private readonly float _fadeOutTime;
		private float _timer;

        public LogicIntro(
			[Param("Font")] string font,
			[Param("Text")] string text,
			[Param("TextColor")] Color textColor,
			[Param("FadeInTime")] float fadeInTime,
			[Param("HoldTime")] float holdTime,
			[Param("FadeOutTime")] float fadeOutTime,
			EntityManager em, SpriteBatch sb) : base(em, sb, fadeInTime + holdTime + fadeOutTime)
		{
			ec = EntityManager.EntityClass.ui;

            _font = ResourceManager.fonts[font];
			_color = textColor;
            _fadeInTime = fadeInTime;
            _holdTime = holdTime;
            _fadeOutTime = fadeOutTime;

            _label = new TextLabel()
			{
				Color = Color.Transparent,
				TextAlign = TextAlign.Center,
				Position = new Vector2(Global.playingFieldWidth/2, Global.screenHeight/2),
				TextGravity = TextGravity.Center,
				Size = new Vector2(-1, -1),
				Font = _font,
				TextString = text,
			};
		}

		public override void Update(GameTime gt)
		{
			base.Update(gt);

			_timer += (float)gt.ElapsedGameTime.TotalSeconds;

			if (_timer < _fadeInTime)
            {
				_label.Color = _color * (_timer / _fadeInTime);
            }
			else if (_timer > _fadeInTime + _holdTime)
            {
				_label.Color = _color * (1 - ((_timer - (_fadeInTime + _holdTime)) / _fadeOutTime));
            }
			else
            {
				_label.Color = _color;
			}
		}

		public override void Draw(GameTime gt)
		{
			_label.Draw(sb);
		}
	}
}

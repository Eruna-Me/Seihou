using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou.Form.Dialog
{
	class LogicDialogBox : LogicEntity
	{
		private readonly Vector2 _location = new(20, 575);
		private readonly Vector2 _dialogBoxSize = new(700, 130);
		private readonly Vector2 _dialogTextLocation = new(120, 35);
		private readonly Vector2 _dialogTextSize = new(565, 100);
		private readonly Vector2 _nameTextLocation = new(15, 0);
		private readonly Vector2 _pfpLocation = new(35, 35);

		private readonly string _pfpTextureName;
		private readonly string _characterName;
		private readonly float _timePerCharacter;
		private readonly float _waitBeforeClose;
		private readonly Color _textColor;

		private readonly StringBuilder _displayedText = new();
		private readonly TextBlock _textBlock;
		private readonly SpriteFont _font;
		private string _allText;
		private float _characterTimer;
		private float _inactiveTimer;
		private float _alpha;
		private int _textIndex;

		public LogicDialogBox(
			[Param("Wpm")]float wpm, 
			[Param("WaitBeforeClose")]float wait,
			[Param("PfpTexture")]string texture,
			[Param("Name")]string name,
			[Param("Font")]string font,
			[Param("Text")]string text,
			[Param("TextColor")]Color textColor,
			EntityManager em, SpriteBatch sb) : base(em, sb)
		{
			ec = EntityManager.EntityClass.ui;

			_textColor = textColor;
			_timePerCharacter = 1 / (wpm / 12);
			_waitBeforeClose = wait;
			_pfpTextureName = texture;
			_characterName = name;
			_font = ResourceManager.fonts[font];
			_allText = text;

			_textBlock = new TextBlock()
			{
				Font = _font,
				TextAlign = TextAlign.Left,
				Position = _location + _dialogTextLocation,
				TextGravity = TextGravity.Top,
				Size = _dialogTextSize,
				ParseOnDraw = false,
				TextString = "",
				SpaceClipDistance = 10,
			};
		}

		public override void Update(GameTime gt)
		{
			GetAlpha();

			if (_textIndex < _allText.Length)
			{
				KeyboardState kb = Keyboard.GetState();
				bool skip = kb.IsKeyDown(Settings.GetKey("dialogSkipKey"));

				_characterTimer += gt.Time() * (skip ? 10 : 1);
				while (_characterTimer > _timePerCharacter)
				{
					_characterTimer -= _timePerCharacter;
					_displayedText.Append(_allText[_textIndex++]);
					_textBlock.TextString = _displayedText.ToString();
					_textBlock.ParseText();

					if (_textIndex >= _allText.Length)
						break;

					if (_textBlock.ParserTrimmedText.Length > 0)
					{
						MoveLine();
					}
				}
			}
			else
			{
				_inactiveTimer += gt.Time();

				if (_inactiveTimer > _waitBeforeClose)
				{
					em.RemoveEntity(this);
				}
			}

			_textBlock.Color = _textColor * _alpha;
		}

		private void MoveLine()
		{
			using StringReader reader = new(_textBlock.ParsedText);
			_displayedText.Remove(0, reader.ReadLine().Length + 1);
			_textBlock.TextString = _displayedText.ToString();
		}

		private void GetAlpha()
		{
			var player = em.GetPlayer();
			var touchingPlayer = new Rectangle((int)_location.X, (int)_location.Y, (int)_dialogBoxSize.X, (int)_dialogBoxSize.Y)
			.Intersects(new Rectangle((int)player.pos.X, (int)player.pos.Y, player.size * 2, player.size * 2));
			_alpha = touchingPlayer ? 0.4f : 1f;
		}

		public override void Draw(GameTime gt)
		{
			var box = ResourceManager.textures["DialogBox"];
			sb.Draw(box, _location, Color.White * _alpha);
			_textBlock.Draw(sb);
			sb.DrawString(_font, _characterName, _location + _nameTextLocation, _textColor * _alpha);
			sb.Draw(ResourceManager.textures[_pfpTextureName], _location + _pfpLocation, Color.White * _alpha);
		}

		public void StartDialog(string text)
		{
			_textIndex = 0;
			_allText = text;
		}
	}
}

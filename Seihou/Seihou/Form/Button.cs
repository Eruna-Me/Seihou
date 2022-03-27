using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;

namespace Seihou
{
    internal class Button : Control
	{
		public event EventHandler OnReleased;
		public event EventHandler OnSelect;

		public Color TextColor { get; set; } = Color.White;
		public Color TextColorSelected { get; set; } = new Color(170,170,170);
		public Color TextColorOnMouseDown { get; set; } = new Color(110,110,110);
		public Color TextColorTabIndex { get; set; } = new Color(230,230,150);
		public Color BackgroundColor { get; set; } = Color.Transparent;
		public Color BackgroundColorSelected { get; set; } = Color.Transparent;
		public Color BackgroundColorOnMouseDown { get; set; } = Color.Transparent;
		public Color BackgroundColorTabIndex { get; set; } = Color.Transparent;

		public TextAlign Align { get; set; } = TextAlign.Center;
		public TextGravity Gravity { get; set; } = TextGravity.Center;

		public Vector2 Position { get; set; }
		public Vector2 Size { get; set; }
		public string Text { get; set; }
		public string Font { get; set; } = "DefaultFont";

		private bool isSelected = false;
		private bool isPressed = false;
		private int lastTabIndex = -1;
		private Vector2 lastMousePosition;
		private Vector2 pressedAt;

		private readonly TextLabel Label = new();

        public Button(SpriteBatch sb) : base(sb)
		{
        }

        public Button(SpriteBatch sb, Action onReleased) : this(sb)
        {
			OnReleased += (_,_) => onReleased();
        }

		public Button(SpriteBatch sb, Action onReleased, Color simpleColor) : this(sb)
		{
			OnReleased += (_, _) => onReleased();
			SetSimpleColorText(simpleColor);
		}

		public void SetSimpleColorText(Color c)
        {
			TextColor = c;
			TextColorSelected = new Color(c.R - 50, c.G - 50, c.B - 50, c.A);
			TextColorOnMouseDown = new Color(c.R - 100, c.G - 100, c.B - 100, c.A);
        }

		private Color GetCurrentBackgroundColor()
        {
			if (isPressed)
				return BackgroundColorOnMouseDown;

			if (lastTabIndex == TabIndex)
				return BackgroundColorTabIndex;

			if (isSelected)
				return BackgroundColorSelected;


			return BackgroundColor;
        }

		private Color GetCurrentTextColor()
		{
			if (isPressed)
				return TextColorOnMouseDown;

			if (lastTabIndex == TabIndex)
				return TextColorTabIndex;

			if (isSelected)
				return TextColorSelected;

			return TextColor;
		}

		public override void Draw(GameTime gt)
		{
            var font = ResourceManager.fonts[Font];

            Primitives2D.FillRectangle(sb, Position, Size, GetCurrentBackgroundColor());

			Label.Font = font;
			Label.TextString = Text;
			Label.Size = Size;
			Label.Color = GetCurrentTextColor();
			Label.Position = Position;
			Label.TextAlign = Align;
			Label.TextGravity = Gravity;
			Label.Draw(sb);
		}

		public override void Update(GameTime gt)
		{
			if (isSelected)
			{
				OnSelect?.Invoke(this, EventArgs.Empty);
			}
		}

		public override void SetTabIndex(int index)
        {
			lastTabIndex = index;
		}

		public override void MouseMove(Vector2 mouse)
        {
			isSelected = Intersects(mouse);

			if (!isSelected)
            {
				isPressed = false;
            }

			lastMousePosition = mouse;
		}

		public override void Press()
        {
			if (Intersects(lastMousePosition))
			{
				isPressed = true;
				pressedAt = lastMousePosition;
			}
		}

        public override void Release()
        {
			if (Intersects(lastMousePosition) && Intersects(pressedAt))
            {
				OnReleased?.Invoke(this, EventArgs.Empty);
			}

			isPressed = false;
        }

		private bool Intersects(Vector2 point)
        {
			return  
				(point.X > Position.X) &&
				(point.X < Position.X + Size.X) &&
				(point.Y > Position.Y) &&
				(point.Y < Position.Y + Size.Y);
		}

        public override void OnKeyboardPress()
        {
            if (TabIndex == lastTabIndex)
            {
				OnReleased?.Invoke(this, EventArgs.Empty);
			}
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seihou
{
	internal class TextField : Button
	{
		public event EventHandler OnSubmit;

		public bool ClearOnClick { get; set; } = true;
		public string KeyName { get; set; }

		private readonly Action<bool> blockUserInput;
		private bool isInEnterMode = false;

		private readonly HashSet<Keys> pressedBuffer = new();

		/// <param name="blockUserInput">Delegate that should disable or enable user input based on the bool passed</param>
		public TextField(Action<bool> blockUserInput, Vector2 pos, Vector2 size, SpriteBatch sb, string text, int index) :
			this(sb, blockUserInput)
		{
			TabIndex = index;
			Size = size;
			Position = pos;
		}

		/// <param name="blockUserInput">Delegate that should disable or enable user input based on the bool passed</param>
		public TextField(SpriteBatch sb, Action<bool> blockUserInput) : base(sb)
		{
			this.blockUserInput = blockUserInput;
			OnReleased += (_, _) => OnClicked();
		}

        protected override Color GetCurrentTextColor()
        {
			if (isInEnterMode)
				return Color.White;

            return base.GetCurrentTextColor();
        }

        private void OnClicked()
		{
			isInEnterMode = true;
			Text = ClearOnClick ? string.Empty : Text;
			blockUserInput(true);
		}

		public override void Update(GameTime gt)
		{
			if (isInEnterMode)
			{
				Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
				Keys[] releasedKeys = Enum.GetValues<Keys>()
					.Where(k => !pressedKeys.Contains(k))
					.ToArray();

				foreach (var key in pressedKeys)
				{
					if (!pressedBuffer.Contains(key))
					{
						pressedBuffer.Add(key);
						PressKey(key);
					}
				}

				foreach (var released in releasedKeys)
				{
					if (pressedBuffer.Contains(released))
						pressedBuffer.Remove(released);
				}
			}

			base.Update(gt);
		}

		private void PressKey(Keys key)
        {
			switch(key)
            {
				case Keys.Enter:
					Submit();
					return;

				case Keys.Back:
					Text = Text.Length > 0 ? Text[..^1] : Text;
					return;

				case Keys.Space: 
					Text += " "; 
					return;

				default:
					if (key >= Keys.A && key <= Keys.Z)
						Text += (char)(int)key;

					break;
			}
		}
		
		private void Submit()
        {
			blockUserInput(false);
			isInEnterMode = false;
			OnSubmit?.Invoke(this, EventArgs.Empty);
		}
	}
}

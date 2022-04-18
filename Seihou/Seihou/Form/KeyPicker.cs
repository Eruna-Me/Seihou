using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Seihou
{
	class KeyPicker : Button
	{
		public Keys Key { get; set; }
		public string KeyName { get; set; }
		public string PrefixText { get; set; }
        private readonly Action<bool> blockUserInput;
		private bool isInEnterMode = false;
		private bool configuredKey = false;

		/// <param name="blockUserInput">Delegate that should disable or enable user input based on the bool passed</param>
        public KeyPicker(Action<bool> blockUserInput, Vector2 pos, Vector2 size, SpriteBatch sb, string text, string keyName, Keys startWith, int index) : 
			this(sb, blockUserInput, startWith, keyName)
		{
			TabIndex = index;
			Size = size;
			Position = pos;
			PrefixText = text;
		}

		/// <param name="blockUserInput">Delegate that should disable or enable user input based on the bool passed</param>
        public KeyPicker(SpriteBatch sb, Action<bool> blockUserInput, Keys startWith, string keyName) : base(sb)
        {
            this.blockUserInput = blockUserInput;
			Key = startWith;
			KeyName = keyName;

			OnReleased += (_,_) => OnClicked();
        }

		private void OnClicked()
        {
			isInEnterMode = true;
			blockUserInput(true);
		}

		public string GetKey() => ((int)Key).ToString();

        protected override Color GetCurrentTextColor()
        {
			if (isInEnterMode)
				return Color.BlueViolet;

            return base.GetCurrentTextColor();
        }

        public override void Update(GameTime gt)
		{
			if (isInEnterMode)
			{
				if (configuredKey)
				{
					if (Keyboard.GetState().IsKeyUp(Key))
                    {
						isInEnterMode = configuredKey = false;
						blockUserInput(false);
                    }
				}
				else
				{
					Keys[] getKeys = Keyboard.GetState().GetPressedKeys();
					if (getKeys.Length > 0 && getKeys[0] != Keys.OemAuto)
					{
						Key = getKeys[0];
						configuredKey = true;
					}
				}
			}

			Text = $"{PrefixText}:  {Key}";

			base.Update(gt);
		}
	}
}

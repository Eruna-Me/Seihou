using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
	class KeyPicker : Control
	{
		Keys Key;
		public string keyName;
		Button button;

		public KeyPicker(Vector2 pos, Vector2 size, SpriteBatch sb, string keyName,Keys startWith) : base(sb)
		{
			this.Key = startWith;
			this.keyName = keyName;
			button = new Button(pos, size, sb, null, keyName, Button.Align.left);
			button.onHover += OnHover;
			this.sb = sb;
		}

		private void OnHover(object sender)
		{
			Keys[] getKeys = Keyboard.GetState().GetPressedKeys();
			if (getKeys.Length > 0)
				Key = getKeys[0];
		}

		public string GetKey() => ((int)Key).ToString();

		public override void Draw(GameTime gt)
		{
			button.Draw(gt);
		}

		public override void Update(GameTime gt)
		{
			button.text = $"{keyName}:  {Key.ToString()}";
			button.Update(gt);
		}
	}
}

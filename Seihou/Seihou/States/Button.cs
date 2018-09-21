using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Seihou
{
	public class Button
	{
		Vector2 pos, size;
		SpriteBatch sb;
		string text;

		public Button(Vector2 pos, Vector2 size, SpriteBatch sb, string text)
		{
			this.pos = pos;
			this.size = size;
			this.sb = sb;
			this.text = text;
		}

		public void Draw(GameTime gt)
		{
			sb.DrawString(ResourceManager.fonts["DefaultFont"], text, pos, Color.White);

			MouseState mouseState = Mouse.GetState();
			if (mouseState.LeftButton == ButtonState.Pressed && ButtonHit())
			{
				sb.DrawString(ResourceManager.fonts["DefaultFont"], $"CRITICAL HIT", new Vector2(300, 200), Color.White);
			}
		}

		public void Update(GameTime gt)
		{
			
		}

		private bool ButtonHit()
		{
			MouseState mouseState = Mouse.GetState();
			if (((mouseState.X > pos.X) && (mouseState.X < pos.X + size.X)) && ((mouseState.Y > pos.Y) && (mouseState.Y < pos.Y + size.Y)))
			{
				return true;
			}
			return false;
		}
	}
}

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
    public delegate void ButtonCallBack();

	public class Button
	{
        public ButtonCallBack bcb;
		Vector2 pos, size;
		SpriteBatch sb;
		string text;
        bool hovering = false;
        bool clicked = false;

		public Button(Vector2 pos, Vector2 size, SpriteBatch sb,ButtonCallBack bcb, string text)
		{
            this.bcb = bcb;
            this.pos = pos - size / 2;
			this.size = size;
			this.sb = sb;
			this.text = text;
		}

		public void Draw(GameTime gt)
		{
            sb.DrawString(ResourceManager.fonts["DefaultFont"], text, pos + size/2, hovering ? Color.White : Color.Gray, 0,new Vector2(10,(ResourceManager.fonts["DefaultFont"].MeasureString(text)/2).Y),1,SpriteEffects.None,0);
		}

		public void Update(GameTime gt)
		{
            MouseState mouseState = Mouse.GetState();
            hovering = MouseOnButton();
            clicked = mouseState.LeftButton == ButtonState.Pressed && hovering;
            if (clicked) bcb();
        }

		private bool MouseOnButton()
		{
			MouseState mouseState = Mouse.GetState();
            return (((mouseState.X > pos.X) && (mouseState.X < pos.X + size.X)) && ((mouseState.Y > pos.Y) && (mouseState.Y < pos.Y + size.Y)));
		}
	}
}

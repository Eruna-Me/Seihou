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
        public enum Align
        {
            left,
            center,
            right,
        }

        public Color SelectedTextColor = Color.White;
        public Color TextColor = Color.Gray;
        public Color background = Color.Transparent;
        public Color background3D = Color.Transparent;
        public Vector2 background3DOffset = new Vector2(4, 4);
        public ButtonCallBack bcb;
		Vector2 pos, size;
		SpriteBatch sb;
		string text;
        string font;
        bool hovering = false;
        bool clicked = false;
        Align align;

		public Button(Vector2 pos, Vector2 size, SpriteBatch sb,ButtonCallBack bcb, string text,Align align = Align.left, string font = "DefaultFont")
		{
            this.font = font;
            this.align = align;
            this.bcb = bcb;
            this.pos = pos - size / 2;
			this.size = size;
			this.sb = sb;
			this.text = text;
		}

		public void Draw(GameTime gt)
		{
            MonoGame.Primitives2D.FillRectangle(sb, pos + background3DOffset, size, background3D, 0);
            MonoGame.Primitives2D.FillRectangle(sb, pos, size, background, 0);
            

            if (Global.drawCollisionBoxes)
                MonoGame.Primitives2D.DrawRectangle(sb, pos, size, Color.Red);

            Vector2 orgin = new Vector2();
            orgin.Y = ResourceManager.fonts[font].MeasureString(text).Y / 2;

            //Current font
            var cf = ResourceManager.fonts[font]; 

            switch (align)
            {
                case Align.left:
                    orgin.X = 0;
                    break;

                case Align.center:
                    orgin.X = cf.MeasureString(text).X / 2;
                    break;

                case Align.right:
                    orgin.X = cf.MeasureString(text).X;
                    break;
            }

            sb.DrawString(cf, text, pos + size/2, hovering ? SelectedTextColor : TextColor, 0,orgin,1,SpriteEffects.None,0);
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

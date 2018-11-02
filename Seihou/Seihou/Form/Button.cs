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
    delegate void ButtonCallBack(object sender);


	class Button : Control
	{
        public enum Align
        {
            left,
            center,
            right,
        }

        public Color selectedTextColor = Color.White;
        public Color background3D = Color.Transparent;
        public Vector2 background3DOffset = new Vector2(4, 4);
        public ButtonCallBack onClicked;
		public ButtonCallBack onHover;

		Vector2 pos, size;
		public string text;
        string font;	
        bool hovering = false;
        Align align;

		public Button(Vector2 pos, Vector2 size, SpriteBatch sb, ButtonCallBack onClicked, string text, Align align = Align.left, string font = "DefaultFont") : base(sb)
		{
			textColor = new Color(100, 100, 100);
            this.font = font;
            this.align = align;
            this.onClicked = onClicked;
            this.pos = pos - size / 2;
			this.size = size;
			this.sb = sb;
			this.text = text;
		}

		public override void Draw(GameTime gt)
		{
            MonoGame.Primitives2D.FillRectangle(sb, pos + background3DOffset, size, background3D, 0);
            MonoGame.Primitives2D.FillRectangle(sb, pos, size, background, 0);
            

            if (Global.drawCollisionBoxes)
                MonoGame.Primitives2D.DrawRectangle(sb, pos, size, Color.Red);

            Vector2 orgin = new Vector2(0,0);
            Vector2 place = new Vector2(0, 0);
            orgin.Y = ResourceManager.fonts[font].MeasureString(text).Y / 2;
            place.Y = size.Y / 2;

            //Current font
            var cf = ResourceManager.fonts[font]; 

            switch (align)
            {
                case Align.left:
                    orgin.X = 0;
                    place.X = 0;
                    break;

                case Align.center:
                    orgin.X = cf.MeasureString(text).X / 2;
                    place.X = size.X / 2;
                    break;

                case Align.right:
                    orgin.X = cf.MeasureString(text).X;
                    place.X = size.X;
                    break;
            }

            sb.DrawString(cf, text,pos + place, hovering ? selectedTextColor : textColor,0,orgin,1,SpriteEffects.None,0);
		}

		public override void Update(GameTime gt)
		{
            MouseState mouseState = Mouse.GetState();
            hovering = MouseOnButton();
            
            if (hovering)
            {
				onHover?.Invoke(this);
				if (Cursor.IsMouseLeftPressed())
                    onClicked?.Invoke(this);
            }
        }

		public void EmptyCall(object sender) { }

		private bool MouseOnButton()
		{
			MouseState mouseState = Mouse.GetState();
            return (((mouseState.X > pos.X) && (mouseState.X < pos.X + size.X)) && ((mouseState.Y > pos.Y) && (mouseState.Y < pos.Y + size.Y)));
		}
	}
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Seihou
{
	class ListBox : Control
    {
        public Vector2 pos;
        public Vector2 size;

        public string[] text = new string[] { "EMPTY" };
        public string font;

        public ListBox(Vector2 pos,Vector2 size,SpriteBatch sb, string font = "DefaultFont") : base(sb)
        {
            this.size = size;
            this.pos = pos;
            this.sb = sb;
            this.font = font;
        }

        public override void Draw(GameTime gt)
        {
            var f = ResourceManager.fonts[font];
            MonoGame.Primitives2D.FillRectangle(sb, pos,size, Color.Beige, 0); //TODO

            for (int i = 0; i < text.Length; i++)
            {
                string txt = text[i];
                sb.DrawString(f,txt,new Vector2(pos.X,pos.Y + f.MeasureString(txt).Y*i+2),Color.Gray); //TODO
            }

        }

        public override void Update(GameTime gt)
        {
            throw new NotImplementedException();
        }
    }
}

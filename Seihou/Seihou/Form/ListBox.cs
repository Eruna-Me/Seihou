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
    class ListBox : Control
    {
        readonly SpriteBatch sb;
        public Vector2 pos;
        public Vector2 size;
        public Color background = Color.Transparent;
        public Color textColor = Color.White;
        public string[] text = new string[] { "EMPTY" };
        public string font;

        public bool Selected { get; private set; }

        public ListBox(Vector2 pos,Vector2 size,SpriteBatch sb, string font = "DefaultFont")
        {
            this.size = size;
            this.pos = pos;
            this.sb = sb;
            this.font = font;
        }

        public override void Draw(GameTime gt)
        {
            var f = ResourceManager.fonts[font];
            MonoGame.Primitives2D.FillRectangle(sb, pos,size, background, 0);

            for (int i = 0; i < text.Length; i++)
            {
                string txt = text[i];
                sb.DrawString(f,txt,new Vector2(pos.X,pos.Y + f.MeasureString(txt).Y*i+2),textColor);
            }
        }

        public override void Update(GameTime gt)
        {
            throw new NotImplementedException();
        }
    }
}

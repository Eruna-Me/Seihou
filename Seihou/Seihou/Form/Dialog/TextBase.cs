using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    public abstract class TextBase
    {
        protected const char SpaceChar = ' ';

        public SpriteFont Font { get; set; }
        public string TextString { get; set; }
        public Color Color { get; set; } = Color.White;
        public TextAlign TextAlign { get; set; } = TextAlign.Left;
        public TextGravity TextGravity { get; set; } = TextGravity.Top;
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
    }
}

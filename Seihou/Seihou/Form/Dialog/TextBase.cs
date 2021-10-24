using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seihou
{
    public abstract class TextBase
    {
        protected const char SpaceChar = ' ';
        protected readonly SpriteFont spriteFont;

        public string TextString { get; set; }
        public Color Color { get; set; } = Color.White;
        public TextAlign TextAlign { get; set; } = TextAlign.Left;
        public TextGravity TextGravity { get; set; } = TextGravity.Top;

        public TextBase(SpriteFont spriteFont)
        {
            this.spriteFont = spriteFont;
        }
    }
}

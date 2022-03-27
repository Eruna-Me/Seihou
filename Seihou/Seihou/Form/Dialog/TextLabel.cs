using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Seihou
{
    public class TextLabel : TextBase
    {
        public TextLabel()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (string.IsNullOrEmpty(TextString))
            {
                return;
            }

            spriteBatch.DrawStringAligned(Font, TextString, Position, Color, TextAlign, TextGravity, Size, 0);
        }
    }
}

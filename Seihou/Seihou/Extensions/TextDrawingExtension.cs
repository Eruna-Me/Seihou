using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seihou
{
    public enum TextAlign
    {
        Right,
        Center,
        Left,
    }

    public enum TextGravity
    {
        Top = -1,
        Center = 0,
        Bottom = 1,
    }

    public static class TextDrawingExtension
    {
        /// <summary>
        /// When size is null size is set to the dimensions of str
        /// </summary>
        public static void DrawStringAligned(this SpriteBatch spriteBatch, 
            SpriteFont font, 
            string str, 
            Vector2 position, 
            Color color, 
            TextAlign align,
            TextGravity gravity = TextGravity.Top,
            Vector2? bounds = null, 
            float layerDepth = 0
            )
        {
            if (str == null)
                return;

            var totalSize = font.MeasureString(str);
            var totalOffset = new Vector2(GetAlignOffset((bounds ?? totalSize).X, align), 0);

            var lines = str.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                (float width, _) = font.MeasureString(lines[i]);

                var offsetX = -GetAlignOffset(width, align);
                var offsetY = GetGravityOffset((bounds ?? Vector2.Zero).Y, gravity) - GetGravityOffset(totalSize.Y, gravity);

                var lineOffset = new Vector2(offsetX, font.LineSpacing * i + offsetY);

                spriteBatch.DrawString(font, lines[i], position + lineOffset + totalOffset, color, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
            }
        }

        #pragma warning disable hidde_complaining_about_duplicate_method
        private static float GetGravityOffset(float height, TextGravity gravity)
        {
            return gravity switch
            {
                TextGravity.Center => height / 2,
                TextGravity.Top => 0,
                TextGravity.Bottom => height,
                _ => throw new InvalidOperationException($"No implementation for \"{gravity}\""),
            };
        }

        private static float GetAlignOffset(float lineWidth, TextAlign align)
        {
            return align switch
            {
                TextAlign.Center => lineWidth / 2,
                TextAlign.Left => 0,
                TextAlign.Right => lineWidth,
                _ => throw new InvalidOperationException($"No implementation for \"{align}\""),
            };
        }
    }
}

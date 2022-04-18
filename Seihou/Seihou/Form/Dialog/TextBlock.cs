using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Seihou
{
    public class TextBlock : TextBase
    {
        /// <summary>
        /// Distance in characters from the left border to search for spaces to replace with newlines (0 = only character on line, -1 = no space clipping)
        /// </summary>
        public int SpaceClipDistance { get; set; } = -1;

        /// <summary>
        /// Run ParseText() on every draw call 
        /// </summary>
        public bool ParseOnDraw { get; set; } = true;

        /// <summary>
        /// Text with added newlines or removed lines to fit boundary (filled after calling ParseText())
        /// </summary>
        public string ParsedText { get; private set; } = null;

        /// <summary>
        /// Text that got removed by the parser (filled after calling ParseText())
        /// </summary>
        public string ParserTrimmedText { get; private set; } = string.Empty;

        /// <summary>
        /// Trimmed characterIndex of TextString
        /// </summary>
        public int ParserTrimmedIndex { get; set; }

        public int MaxLines { get; set; } = -1;

        private Vector2? PreviouslyParsedSize { get; set; }
        private string PreviouslyParsedTextString { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ParseOnDraw) ParseText();

            spriteBatch.DrawStringAligned(Font, ParsedText ?? TextString, Position, Color, TextAlign, TextGravity, Size, 0);
        }

        public void ParseText()
        {
            if (TextString == PreviouslyParsedTextString && Size == PreviouslyParsedSize)
                return;

            if (Size.X < 0 || Size.Y < 0)
            {
                ParsedText = string.Empty;
                ParserTrimmedText = TextString;
                return;
            }

            ParsedText = ParseText(TextString, Size.X, Size.Y, out string trimmedText, out int trimmedIndex);

            ParserTrimmedIndex = trimmedIndex;
            ParserTrimmedText = trimmedText;

            PreviouslyParsedSize = new Vector2(Size.X, Size.Y);
            PreviouslyParsedTextString = TextString;
        }

        public float GetRequiredHeight(float width)
        {
            var parsedText = ParseText(TextString, width, int.MaxValue, out _, out _);
            return Font.MeasureString(parsedText).Y;
        }

        private string ParseText(string text, float width, float height, out string trimmedText, out int trimmedIndex)
        {
            trimmedIndex = 0;
            int maxPossibleLines = Math.Min((int)(height / Font.LineSpacing), MaxLines == -1 ? int.MaxValue : MaxLines);
            List<string> lines = text.Split('\n').Take(maxPossibleLines).ToList();
            var insertedNewlines = 0;

            for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
            {
                var line = lines[lineIndex];
                var overlap = GetXOverlapIndex(line, width);

                if (overlap == 0)
                {
                    trimmedText = text;
                    return string.Empty;
                }

                if (overlap != -1)
                {
                    bool isLastLine = lineIndex == maxPossibleLines - 1;
                    var fixedLine = HandleOverlap(line, overlap, isLastLine);
                    var parts = fixedLine.Split('\n');

                    Debug.Assert(parts.Length == 2, "More than one newline added");

                    lines[lineIndex] = parts.First();

                    if (isLastLine) break;

                    insertedNewlines += fixedLine.Length - line.Length;
                    lines.Insert(lineIndex + 1, parts.Last());
                }
            }

            var parsedText = string.Join('\n', lines.Take(maxPossibleLines));
            trimmedIndex = Math.Min(text.Length, parsedText.Length - insertedNewlines);
            trimmedText = text[trimmedIndex..];
            return parsedText;
        }

        private int GetXOverlapIndex(string line, float boundary)
        {
            if (line.Length == 0)
                return -1;

            var offset = Vector2.Zero;
            var firstGlyphOfLine = true;
            var gly = Font.GetGlyphs();

            for (var i = 0; i < line.Length; ++i)
            {
                var c = line[i];

                if (c == '\r')
                    continue;

                var g = gly[c];

                if (firstGlyphOfLine)
                {
                    offset.X = Math.Max(g.LeftSideBearing, 0);
                    firstGlyphOfLine = false;
                }
                else
                {
                    offset.X += Font.Spacing + g.LeftSideBearing;
                }

                offset.X += g.Width;

                var width = offset.X + Math.Max(g.RightSideBearing, 0);
                if (width > boundary)
                    return i;

                offset.X += g.RightSideBearing;
            }
            return -1;
        }

        private string HandleOverlap(string line, int overlapIndex, bool isLastLine)
        {
            int space = FindPreviousSpace(line, overlapIndex); //TODO: no need to do this if its the last line..

            return (space == -1 || isLastLine)
                ? line.Insert(overlapIndex, "\n") //Not clipping to space on last line 
                : line.Remove(space, 1).Insert(space, '\n'.ToString());
        }

        private int FindPreviousSpace(string str, int startfrom)
        {
            for (int i = 0; i < SpaceClipDistance; i++)
            {
                var index = startfrom - i;
                if (index < 0) return -1;
                char c = str[index];
                if (c == '\n') return -1;
                if (c == SpaceChar) return index;
            }
            return -1;
        }
    }
}
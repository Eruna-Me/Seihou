using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seihou
{
    internal class TextBox : TextBlock
    {
        public int SelectionIndex { get; private set; } = 0;

        public void EnterKey(Keys key)
        {
            switch(key)
            {
                case Keys.Back:
                    if (SelectionIndex > 1) TextString = TextString.Remove(SelectionIndex - 1,1);
                    return;

                case Keys.Enter:
                    TextString = TextString.Insert(SelectionIndex, "\n");
                    SelectionIndex++;
                    return;

                case Keys.Right:
                    MoveHorizontal(1);
                    return;

                case Keys.Left:
                    MoveHorizontal(-1);
                    return;

                default:
                    TextString = TextString.Insert(SelectionIndex, key.ToString());
                    break;
            }
        }

        private void MoveHorizontal(int direction)
        {
            var newPos = SelectionIndex + direction;
            if (newPos < 0 || newPos > TextString.Length - 1)
                SelectionIndex = newPos;
        }
    }
}

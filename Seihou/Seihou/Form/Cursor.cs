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
    static class Cursor
    {
        static MouseState? lastState = null;

        public static bool IsMouseLeftPressed()
        {
            bool clicked = false;
            var currentState = Mouse.GetState();
            if (lastState != null)
            {
                if (currentState.LeftButton == ButtonState.Pressed && lastState.Value.LeftButton == ButtonState.Released)
                {
                    clicked = true;
                }
            }
            lastState = currentState;
            return clicked;
        }
    }
}

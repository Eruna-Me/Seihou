using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
	static class Cursor
    {
		static Vector2 lastMousePos = new Vector2(0,0);
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

		public static void Moved()
		{
			MouseState mouseState = Mouse.GetState();
			if(mouseState.X != lastMousePos.X || mouseState.Y != lastMousePos.Y )
			{
				Global.keyMode = false;
			}
			lastMousePos = new Vector2(mouseState.X, mouseState.Y);
		}
    }
}

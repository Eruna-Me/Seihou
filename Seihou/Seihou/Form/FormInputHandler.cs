using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Seihou
{
    internal class FormInputHandler
    {
        public bool BlockKeyboard { get; set; } = false;
        public bool Block { get; set; } = false;

        public event Action<int> OnTabIndexChanged;
        public event Action<Vector2> OnMouseMoved;
        public event Action OnPressed;
        public event Action OnReleased;
        public event Action OnKeyboardPress;
        public Vector2 LastPosition { get; set; }
        
        private MouseState lastMouseState = new();
        private KeyboardState lastKeyboardState = new();

        public void Update()
        {
            if (Block)
            {
                lastMouseState = new();
            }
            else
            {
                UpdateMouse();
            }

            if (Block || BlockKeyboard)
            {
                lastKeyboardState = new();
            }
            else
            {
                UpdateKeyboard();
            }
        }

        public void UpdateKeyboard()
        {
            var state = Keyboard.GetState();

            bool lastUp = lastKeyboardState.IsKeyUp(Settings.GetKey("upKey"));
            bool lastDown = lastKeyboardState.IsKeyUp(Settings.GetKey("downKey"));
            bool lastPressed = lastKeyboardState.IsKeyUp(Settings.GetKey("shootKey"));

            bool nowUp = state.IsKeyUp(Settings.GetKey("upKey"));
            bool nowDown = state.IsKeyUp(Settings.GetKey("downKey"));
            bool nowPressed = state.IsKeyUp(Settings.GetKey("shootKey"));

            if (!lastPressed && nowPressed)
            {
                OnTabIndexChanged?.Invoke(0);
                OnKeyboardPress?.Invoke();
            }

            if (!lastUp && nowUp)
                OnTabIndexChanged?.Invoke(-1);
            
            if (!lastDown && nowDown)
                OnTabIndexChanged?.Invoke(+1);
            
            lastKeyboardState = state;
        }

        public void UpdateMouse()
        {
            var state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
            {
                OnPressed?.Invoke();
            }

            if (state.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                OnReleased?.Invoke();

            var position = state.Position.ToVector2();

            if (position != LastPosition)
                OnMouseMoved?.Invoke(position);

            LastPosition = position;
            lastMouseState = state;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Seihou
{
	class MenuState : State
	{
		const int firstButtonHeight = 80;
		const int buttonX = 50;
		const int buttonHeight = 30;
		int y;
		Button bob;

		public MenuState (StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
			bob = new Button(new Vector2(400, 200), new Vector2(300, 100), sb, "O hi mark");
		}

		public override void Draw(GameTime gt)
		{
			y = firstButtonHeight - buttonHeight;
			sb.DrawString(ResourceManager.fonts["DefaultFont"], "O hi mark", new Vector2(buttonX, y+= buttonHeight), Color.White);
			sb.DrawString(ResourceManager.fonts["DefaultFont"], "O hi mark", new Vector2(buttonX, y += buttonHeight), Color.White);
			sb.DrawString(ResourceManager.fonts["DefaultFont"], "O hi mark", new Vector2(buttonX, y += buttonHeight), Color.White);

			MouseState mouseState = Mouse.GetState();
			if (mouseState.LeftButton == ButtonState.Pressed)
			{
				sb.DrawString(ResourceManager.fonts["DefaultFont"], $"X {mouseState.X} Y {mouseState.Y}", new Vector2(200, 200), Color.White);
			}

			bob.Draw(gt);
		}

		public override void Update(GameTime gt)
		{
			bob.Update(gt);
		}

		public override void OnStart()
		{
		}

		public override void OnExit()
		{
		}
	}
}

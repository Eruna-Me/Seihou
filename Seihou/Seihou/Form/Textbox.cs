using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace Seihou
{
	class OnScreenKeyboard : Control
    {
        const int maxLength = 15;

        public string text = "";

		bool done = false;
        

        string[,] keys = { {"A","B","C","D","E","F","G","H","I","J","K","L","M","OK"},
                         {"N","O","P","Q","R","S","T","U","V","W","X","Y","Z","<"} };
        Vector2 pos;

        List<Button> buttons = new List<Button>();
		readonly Action onSubmit;

        const int xSpacing = 40;
		const int ySpacing = 40;

        public OnScreenKeyboard(Vector2 pos, SpriteBatch sb,Action onSubmit) : base(sb)
        {
			this.onSubmit = onSubmit;
            this.pos = pos;

            for (int row = 0; row < keys.GetLength(0); row++)
            {
				for (int key = 0; key < keys.GetLength(1); key++)
				{
					var button = new Button(sb)
					{
						Position = new Vector2(pos.X + key * xSpacing, pos.Y + (row + 1) * ySpacing),
						Size = new Vector2(xSpacing, ySpacing),
						Text = keys[row, key].ToString(),
						BackgroundColor = Color.Gray
					};

                    button.OnReleased += Pressed;
				}
            }

			foreach (var b in buttons)
			{
				switch(b.Text)
				{
					case "OK":
						b.TextColor = Color.Green;
						break;
					case "<":
						b.BackgroundColor = Color.Red;
						break;
				}
			}
        }

        public override void Draw(GameTime gt)
        {
			if (!done)
			{
				sb.DrawString(ResourceManager.fonts["DefaultFont"], text, pos, Color.White, 0, new Vector2(0, ResourceManager.fonts["DefaultFont"].MeasureString(text).Y / 2), 1, SpriteEffects.None, 0);
				foreach (var b in buttons) b.Draw(gt);
			}
			else
			{
				sb.DrawString(ResourceManager.fonts["DefaultFont"],"Score submitted!", new Vector2(pos.X + 100,pos.Y+125), Color.White, 0, new Vector2(0, ResourceManager.fonts["DefaultFont"].MeasureString("Score submitted!").Y / 2), 1, SpriteEffects.None, 0);
			}

        }

        public override void Update(GameTime gt)
        {
			foreach (var b in buttons) b.Update(gt);
        }

        public void BackSpace()
        {
			if (text.Length > 0)
			{
				text = text.Substring(0, text.Length - 1);
			}
        }

        private void Pressed(object sender, EventArgs e)
		{
			string input = ((Button)sender).Text;

			switch(input)
			{
				case "<":
					BackSpace();
					break;

				case "OK":
					if (text.Length > 0)
					{
						done = true;
						onSubmit?.Invoke();
					}
					break;

				default:
					if (text.Length < maxLength)
						text += input;
					break;
			}
        }
    }
}

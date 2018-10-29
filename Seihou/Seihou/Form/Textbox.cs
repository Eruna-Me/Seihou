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
    class Textbox : Control
    {
        const int maxLength = 15;

        public string text = "";
        

        char[,] keys = { {'A','B','C','D','E','F','G','H','I','J','K','L','M'},
                         {'N','O','P','Q','R','S','T','U','V','W','X','Y','Z'} };
        Vector2 pos;

        SpriteBatch sb;
        List<Button> buttons = new List<Button>();

        int xSpacing = 40;
        int ySpacing = 40;

        public Textbox(Vector2 pos, SpriteBatch sb) : base(sb)
        {
            this.pos = pos;
            this.sb = sb;
            for (int row = 0; row < keys.GetLength(0); row++)
            {
                for (int key = 0; key < keys.GetLength(1); key++)
                {
                    buttons.Add(new Button(new Vector2(pos.X + key * xSpacing, pos.Y + (row+1) * ySpacing), new Vector2(xSpacing, ySpacing), sb, Pressed, keys[row, key].ToString(),Button.Align.center));
                }
            }
        }

        public override void Draw(GameTime gt)
        {
			sb.DrawString(ResourceManager.fonts["DefaultFont"], text, pos, Color.White,0,new Vector2(0,ResourceManager.fonts["DefaultFont"].MeasureString(text).Y/2),1,SpriteEffects.None,0);
            foreach (var b in buttons) b.Draw(gt);
        }

        public override void Update(GameTime gt)
        {
            foreach (var b in buttons) b.Update(gt);
        }

        public void BackSpace(object sender)
        {
			if (text.Length > 1)
			{
				text.Substring(0, text.Length - 1);
			}
        }

        private void Pressed(object sender)
        {
            if (text.Length < maxLength)
                text += ((Button)sender).text;
        }
    }
}

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
	class AboutState : State
	{


		public AboutState (StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {

		}

		public override void Draw(GameTime gt)
		{
			var f = ResourceManager.fonts["DefaultFont"];
			var text = "SEIHOU\n Made in C# \n\nThis game was made by \nDennis & Hidde\nFor Fritz B\n.";
			sb.DrawString(f,text, new Vector2(Global.screenWidth / 2, 200), Color.White, 0, f.MeasureString(text)/2, 1, SpriteEffects.None, 0);
        }

		public override void Update(GameTime gt)
		{
		}
	}
}

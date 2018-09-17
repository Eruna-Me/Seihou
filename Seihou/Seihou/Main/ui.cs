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
	public static class UI
	{
		public static void Draw(GameTime gt, SpriteBatch sb)
		{
			MonoGame.Primitives2D.FillRectangle(sb, Global.playingFieldWidth, 0, Global.uiWidth, Global.screenHeight, Color.Blue);
		}
	}
}

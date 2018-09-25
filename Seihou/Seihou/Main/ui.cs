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
			const int uiLineHeight = 30;
			MonoGame.Primitives2D.FillRectangle(sb, Global.playingFieldWidth, 0, Global.uiWidth, Global.screenHeight, new Color(60,60,60));

			int y = 0;
			sb.DrawString(ResourceManager.fonts["DefaultFont"], "O hi mark", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White);
			sb.DrawString(ResourceManager.fonts["DefaultFont"], Global.player.lives.ToString(), new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.Red);
			sb.DrawString(ResourceManager.fonts["DefaultFont"], Global.player.score.ToString(), new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.Red);
			sb.DrawString(ResourceManager.fonts["DefaultFont"], Global.player.collectedPowerUps.ToString(), new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.Red);
		}
	}
}

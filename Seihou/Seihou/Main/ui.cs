﻿using System;
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
            var font1 = ResourceManager.fonts["DefaultFont"];
            var font2 = ResourceManager.fonts["DefaultFontBig"];
            

			const int uiLineHeight = 30;
			MonoGame.Primitives2D.FillRectangle(sb, Global.playingFieldWidth, 0, Global.uiWidth, Global.screenHeight, new Color(60,60,60));
            MonoGame.Primitives2D.FillRectangle(sb, Global.playingFieldWidth, 20, Global.uiWidth, Global.screenHeight-40, new Color(80, 80, 80));

            sb.Draw(ResourceManager.textures["SeihouText1"], new Vector2(Global.uiWidth, 400), Color.White);
            sb.Draw(ResourceManager.textures["Tree"], new Vector2(Global.uiWidth+290, 20), Color.White);

            int y = 0;

            //Score
            sb.DrawString(font2, "Score: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White);
            sb.DrawString(font2, Global.player.score.ToString(), new Vector2(Global.playingFieldWidth + 20 + font2.MeasureString("Score: ").X, y), Color.Gray);

            y += uiLineHeight; //New line

            //Lives       
            sb.DrawString(font1, "Lives: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White);
            sb.DrawString(font1, "Lenovo".Substring(0,Math.Max(Global.player.lives,0)), new Vector2(Global.playingFieldWidth + 20 + font1.MeasureString("Lives: ").X, y), new Color(0,240,0));

            y += uiLineHeight; //New line

            //Powerups
            sb.DrawString(font1, "Power: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White);
            sb.DrawString(font1, Global.player.collectedPowerUps.ToString(), new Vector2(Global.playingFieldWidth + 20 + font1.MeasureString("Power: ").X, y), Color.Red);

            //Grace
            sb.DrawString(font1, "Grace: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White);
            sb.DrawString(font1, Global.player.collectedPowerUps.ToString(), new Vector2(Global.playingFieldWidth + 20 + font1.MeasureString("Grace: ").X, y), Color.Blue);

            y += uiLineHeight; //New line

            //Grace
            sb.DrawString(font1, "Stage: 1", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White);
        }
    }
}

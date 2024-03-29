﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Seihou
{
	internal static class UI
	{
		public static void Draw(GameTime gt, SpriteBatch sb, StateManager sm, EntityManager em, LevelManager levelManager)
		{
            var font1 = ResourceManager.fonts["DefaultFont"];
            var font2 = ResourceManager.fonts["DefaultFontBig"];
            
			const int uiLineHeight = 30;

			sb.Draw(ResourceManager.textures["backgroundUI"], new Vector2(Global.playingFieldWidth, 0), Color.White);
			sb.Draw(ResourceManager.textures["SeihouText1"], new Vector2(Global.playingFieldWidth, 400), Color.White);
            

            int y = 0;

			//Score
			DrawOutlinedFont(font2, "Score: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White, Color.Black);
			DrawOutlinedFont(font2, Math.Round(Global.player.score).ToString(), new Vector2(Global.playingFieldWidth + 20 + font2.MeasureString("Score: ").X, y), Color.White, Color.Black);

            y += uiLineHeight; //New line

			//Lives       
			DrawOutlinedFont(font1, "Lives: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White, Color.Black);
 
			for (int i = 0; i < Global.player.lives; i++)
			{
				sb.Draw(ResourceManager.textures["Heart"], new Vector2(Global.playingFieldWidth + 120 + i * (ResourceManager.textures["Heart"].Width + 10), y), Color.White);
			}

            y += uiLineHeight; //New line

			//Bombs
			DrawOutlinedFont(font1, "Bombs: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White, Color.Black);

			for (int i = 0; i < Global.player.bombs; i++)
			{
				sb.Draw(ResourceManager.textures["Bomb"], new Vector2(Global.playingFieldWidth + 120 + i * (ResourceManager.textures["Bomb"].Width + 10), y), Color.White);
			}

			y += uiLineHeight; //New line

			//Powerups 
			string powerText = (Global.player.power >= Global.player.fullPower) ? "Max" : Global.player.power.ToString();

			DrawOutlinedFont(font1, "Power: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White, Color.Black);
			DrawOutlinedFont(font1,powerText, new Vector2(Global.playingFieldWidth + 20 + font1.MeasureString("Power: ").X, y), Color.Red, Color.Black);

			//Graze
			DrawOutlinedFont(font1, "Graze: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White, Color.Black);
			DrawOutlinedFont(font1, Math.Round(Global.player.graze).ToString(), new Vector2(Global.playingFieldWidth + 20 + font1.MeasureString("Grace: ").X, y), Color.Blue, Color.Black);

            y += uiLineHeight; //New line

			//Current level
			DrawOutlinedFont(font1, levelManager.CurrentLevelName, new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White, Color.Black);

			//Fps

#if DEBUG
			sb.DrawString(font1, $"Fps: {sm.GetFps()}", Global.FpsCounterPos, CoolFpsColorThing(sm.GetFps()));
            sb.DrawString(font1, $"Entities: {em.GetEntityCount()}", Global.EntCounterPos, Color.Black);

            //Make the fps counter the proper color
            Color CoolFpsColorThing(float fps)
            {
                if (fps > 50.0f) { return new Color(0, 255, 0); }
                if (fps > 30.0f) { return Color.Orange; }
                return Color.Red;
            }
#endif

			void DrawOutlinedFont(SpriteFont font, string text, Vector2 pos, Color color, Color outline, int thickness = 1)
			{
				sb.DrawString(font1, text, new Vector2(pos.X, pos.Y + thickness), outline);
				sb.DrawString(font1, text, new Vector2(pos.X, pos.Y - thickness), outline);
				sb.DrawString(font1, text, new Vector2(pos.X + thickness, pos.Y), outline);
				sb.DrawString(font1, text, new Vector2(pos.X - thickness, pos.Y), outline);
				sb.DrawString(font1, text, new Vector2(pos.X, pos.Y), color);
			}
		}
    }
}

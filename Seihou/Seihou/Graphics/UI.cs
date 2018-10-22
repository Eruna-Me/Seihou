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
		public static void Draw(GameTime gt, SpriteBatch sb,StateManager sm,EntityManager em)
		{
            var font1 = ResourceManager.fonts["DefaultFont"];
            var font2 = ResourceManager.fonts["DefaultFontBig"];
            
			const int uiLineHeight = 30;

			sb.Draw(ResourceManager.textures["backgroundUI"], new Vector2(Global.playingFieldWidth, 0), Color.White);
			sb.Draw(ResourceManager.textures["SeihouText1"], new Vector2(Global.uiWidth, 400), Color.White);
            

            int y = 0;

			//Score
			DrawOutlinedFont(font2, "Score: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White, Color.Black);
			DrawOutlinedFont(font2, Math.Round(Global.player.score).ToString(), new Vector2(Global.playingFieldWidth + 20 + font2.MeasureString("Score: ").X, y), Color.White, Color.Black);

            y += uiLineHeight; //New line

			//Lives       
			DrawOutlinedFont(font1, "Lives: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White, Color.Black);
 
			for (int i = 0; i < Global.player.lives; i++)
			{
				sb.Draw(ResourceManager.textures[Global.player.texture], new Vector2(Global.uiWidth + 120 + i * ResourceManager.textures[Global.player.texture].Width, y), Color.White);
			}

            y += uiLineHeight; //New line

            //Powerups 
            string powerText = (Global.player.power >= Global.player.fullPower) ? "max" : Global.player.power.ToString();

			DrawOutlinedFont(font1, "Power: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White, Color.Black);
			DrawOutlinedFont(font1,powerText, new Vector2(Global.playingFieldWidth + 20 + font1.MeasureString("Power: ").X, y), Color.Red, Color.Black);

			//Grace
			DrawOutlinedFont(font1, "Graze: ", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White, Color.Black);
			DrawOutlinedFont(font1, Math.Round(Global.player.graze).ToString(), new Vector2(Global.playingFieldWidth + 20 + font1.MeasureString("Grace: ").X, y), Color.Blue, Color.Black);

            y += uiLineHeight; //New line

			//Grace
			DrawOutlinedFont(font1, "Stage: 1", new Vector2(Global.playingFieldWidth + 20, y += uiLineHeight), Color.White, Color.Black);

            //Fps
            sb.DrawString(font1, $"Fps: {sm.GetFps()}", Global.FpsCounterPos, CoolFpsColorThing(sm.GetFps()));
            sb.DrawString(font1, $"Entities: {em.GetEntityCount()}", Global.EntCounterPos, Color.Black);

            //Make the fps counter the proper color
            Color CoolFpsColorThing(float fps)
            {
                if (fps > 50.0f) { return new Color(0, 255, 0); }
                if (fps > 30.0f) { return Color.Orange; }
                return Color.Red;
            }

			void DrawOutlinedFont(SpriteFont font, string text, Vector2 pos, Color color, Color outline, int thickness = 2)
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

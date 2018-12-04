using System;
using Microsoft.Xna.Framework;

namespace Seihou
{
    partial class MainState : State
    {
        //Pause buttons
        Button[] pauseMenu = new Button[3];
        Button[] deathMenu = new Button[3];
        int buttonSpacing = 30;

        public void UpdateDeathMenu(GameTime gt)
        {
			Global.buttonCount = deathMenu.Length;
            foreach (var b in deathMenu) b.Update(gt);
        }

        public void DrawDeathMenu(GameTime gt)
        {
            MonoGame.Primitives2D.FillRectangle(sb, new Vector2(0, 0), new Vector2(Global.screenWidth, Global.screenHeight), new Color(Color.Black,0.6f), 0);
            foreach (var b in deathMenu) b.Draw(gt);
        }

        public void UpdatePauseMenu(GameTime gt)
        {
			Global.buttonCount = pauseMenu.Length;
			foreach (var b in pauseMenu) b.Update(gt);
        }

        public void DrawPauseMenu(GameTime gt)
        {
            MonoGame.Primitives2D.FillRectangle(sb, new Vector2(0, 0), new Vector2(Global.screenWidth, Global.screenHeight), new Color(Color.Black, 0.6f), 0);
            foreach (var b in pauseMenu) b.Draw(gt);
        }

        public void BuildMenus()
        {
			int width = 500;
            deathMenu[0] = new Button(new Vector2(Global.Center.X,Global.Center.Y - buttonSpacing*2), new Vector2(width, buttonSpacing), sb, OnClickedContinue, "Continue", 0, Button.Align.center);
            deathMenu[1] = new Button(new Vector2(Global.Center.X,Global.Center.Y), new Vector2(width, buttonSpacing), sb, OnClickedMenuScore, "Menu/Save score", 1, Button.Align.center);
            deathMenu[2] = new Button(new Vector2(Global.Center.X,Global.Center.Y + buttonSpacing*2), new Vector2(width, buttonSpacing), sb, OnClickedExit, "Exit", 2, Button.Align.center);

            pauseMenu[0] = new Button(new Vector2(Global.Center.X,Global.Center.Y - buttonSpacing*2), new Vector2(width, buttonSpacing), sb, OnClickedResume, "Resume", 0, Button.Align.center);
            pauseMenu[1] = new Button(new Vector2(Global.Center.X,Global.Center.Y), new Vector2(width, buttonSpacing), sb, OnClickedMenuScore, "Menu/Save score", 1, Button.Align.center);
            pauseMenu[2] = new Button(new Vector2(Global.Center.X,Global.Center.Y + buttonSpacing*2), new Vector2(width, buttonSpacing), sb, OnClickedExit, "Exit", 2, Button.Align.center);

            foreach (var b in deathMenu) { b.background = new Color(60, 60, 60); b.background3D = Color.Black; }
            foreach (var b in pauseMenu) { b.background = new Color(60, 60, 60); b.background3D = Color.Black; }
        }

        public void OnClickedContinue(object sender)
        {
			Global.player.Continue();
            death = false;
        }

        public void OnClickedResume(object sender)
        {
            pause = false;
        }

        public void OnClickedExit(object sender)
        {
            sm.abort = true;
        }

		public void OnClickedMenuScore(object sender)
		{
			sm.ChangeState(new QuestionState(sm, cm, sb, gdm, Math.Round(Global.player.score), Enum.GetName(typeof(Settings.Difficulty), Settings.GetDifficulty())));
		}
	}
}

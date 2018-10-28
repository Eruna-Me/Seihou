using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

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
            foreach (var b in deathMenu) b.Update(gt);
        }

        public void DrawDeathMenu(GameTime gt)
        {
            MonoGame.Primitives2D.FillRectangle(sb, new Vector2(0, 0), new Vector2(Global.screenWidth, Global.screenHeight), new Color(Color.Black,0.6f), 0);
            foreach (var b in deathMenu) b.Draw(gt);
        }

        public void UpdatePauseMenu(GameTime gt)
        {
            foreach (var b in pauseMenu) b.Update(gt);
        }

        public void DrawPauseMenu(GameTime gt)
        {
            MonoGame.Primitives2D.FillRectangle(sb, new Vector2(0, 0), new Vector2(Global.screenWidth, Global.screenHeight), new Color(Color.Black, 0.6f), 0);
            foreach (var b in pauseMenu) b.Draw(gt);
        }

        public void BuildMenus()
        {
            deathMenu[0] = new Button(new Vector2(Global.Center.X,Global.Center.Y - buttonSpacing*2), new Vector2(200, buttonSpacing), sb, OnClickedContinue, "Continue", Button.Align.center);
            deathMenu[1] = new Button(new Vector2(Global.Center.X,Global.Center.Y), new Vector2(200, buttonSpacing), sb, OnClickedMenu, "Menu", Button.Align.center);
            deathMenu[2] = new Button(new Vector2(Global.Center.X,Global.Center.Y + buttonSpacing*2), new Vector2(200, buttonSpacing), sb, OnClickedExit, "Exit", Button.Align.center);

            pauseMenu[0] = new Button(new Vector2(Global.Center.X,Global.Center.Y - buttonSpacing*2), new Vector2(200, buttonSpacing), sb, OnClickedResume, "Resume", Button.Align.center);
            pauseMenu[1] = new Button(new Vector2(Global.Center.X,Global.Center.Y), new Vector2(200, buttonSpacing), sb, OnClickedMenu, "Menu", Button.Align.center);
            pauseMenu[2] = new Button(new Vector2(Global.Center.X,Global.Center.Y + buttonSpacing*2), new Vector2(200, buttonSpacing), sb, OnClickedExit, "Exit", Button.Align.center);

            foreach (var b in deathMenu) { b.background = new Color(60, 60, 60); b.background3D = Color.Black; }
            foreach (var b in pauseMenu) { b.background = new Color(60, 60, 60); b.background3D = Color.Black; }
        }

        public void OnClickedContinue(object sender)
        {
            Global.player.lives = Settings.startingLives;
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

        public void OnClickedMenu(object sender)
        {
            sm.ChangeState(new MenuState(sm, cm, sb, gdm));
        }
    }
}

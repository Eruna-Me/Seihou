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
    class GameoverState : State
    {
        const int firstButtonHeight = 30;
        Button[] buttons = new Button[2];

        void OnClickedMenu()
        {
            sm.ChangeState(new TestState(sm, cm, sb, gdm));
        }

        void OnClickedExit()
        {
            sm.abort = true;
        }


        public GameoverState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
            int s = 80; //Spacing
            int i = 0;

            buttons[i++] = new Button(new Vector2(Global.Center.X, firstButtonHeight + s * i), new Vector2(300, 50), sb, OnClickedMenu, "Menu");
            buttons[i++] = new Button(new Vector2(Global.Center.X, firstButtonHeight + s * i), new Vector2(300, 50), sb, OnClickedExit, "Exit");

        }

        public override void Draw(GameTime gt)
        {
            foreach (Button b in buttons) b.Draw(gt);
            sb.DrawString(ResourceManager.fonts["DefaultFont"], "O hi mark. you tearing me apart lisa", new Vector2(30, 30), new Color(4, 4, 4));
            sb.Draw(ResourceManager.textures["Logo"], new Vector2(200, 200), Color.White);
        }

        public override void Update(GameTime gt)
        {
            foreach (Button b in buttons) b.Update(gt);
        }
    }
}
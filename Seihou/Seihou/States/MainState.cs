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
        //Variables
        EntityManager em = new EntityManager();
        LevelManager lm;
        SpriteFont font1;
        Player player;
        KeyboardState oldKeyState;

		bool infiniteMode;
        bool pause = false;
        bool death = false;

        //Constructor
        public MainState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm,bool infiniteMode = false) : base(sm, cm, sb, gdm)
        {
			this.infiniteMode = infiniteMode;
            BuildMenus();
            lm = new LevelManager(em);
            font1 = ResourceManager.fonts["DefaultFont"];
            player = new LenovoDenovoMan(sb, em, this.sm, this);
            Global.player = player;
        }

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.FillRectangle(sb, new Vector2(0, 0), new Vector2(Global.playingFieldWidth, Global.screenHeight),Global.gameBackgroundColor, 0);
            em.Draw(gt);
            UI.Draw(gt, sb, sm, em);

            if (pause) DrawPauseMenu(gt);
            if (death) DrawDeathMenu(gt);
            //DrawDennis();
        }

        public override void Update(GameTime gt)
        {
            KeyboardState currentKeyState = Keyboard.GetState();

            if ((currentKeyState.IsKeyDown(Global.PauseKey1) && oldKeyState.IsKeyUp(Global.PauseKey1)) || (oldKeyState.IsKeyUp(Global.PauseKey2) && currentKeyState.IsKeyDown(Global.PauseKey2)))
                pause = !pause;

            oldKeyState = currentKeyState;

            if (!(pause || death))
            {
                lm.Update(gt);
                em.Update(gt);
				CloudManager.Update(gt, sb, em);
            }

            if (pause) UpdatePauseMenu(gt);
            if (death) UpdateDeathMenu(gt);
        }

        public void OnPlayerDeath() => death = true;

        //When the state starts
        public override void OnStart()
        {
            em.AddEntity(player);
            lm.LoadLevel(new DemoLevel(sb, em));
        }

        //When the state exits
        public override void OnExit()
        {
            Debugging.Write(this,"Rip");
        }
    }
}
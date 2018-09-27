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
    class TestState : State
    {
        EntityManager em = new EntityManager();
        LevelManager lm;
        SpriteFont font1;
        Player player;
		KeyboardState oldKeyState;

		bool pause = false;

        public TestState (StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
            //Load content
            lm = new LevelManager(em);
            font1 = ResourceManager.fonts["DefaultFont"];
            player = new Player(sb, em);
			Global.player = player;
        }

        public override void Draw(GameTime gt)
        {
            em.Draw(gt);
            UI.Draw(gt, sb);
            sb.DrawString(font1,$"Fps: {sm.GetFps()}",Global.FpsCounterPos,CoolFpsColorThing(sm.GetFps()));
            sb.DrawString(font1, $"Entities: {em.GetEntityCount()}",Global.EntCounterPos, Color.White);
        }
        
        public override void Update(GameTime gt)
        {
			KeyboardState currentKeyState = Keyboard.GetState();

			if ((currentKeyState.IsKeyDown(Global.PauseKey1) && oldKeyState.IsKeyUp(Global.PauseKey1)) || (oldKeyState.IsKeyUp(Global.PauseKey2) && currentKeyState.IsKeyDown(Global.PauseKey2)))
			{
				pause = !pause;
			}
			oldKeyState = currentKeyState;

			if (!pause)
            { 
                lm.Update(gt);
                em.Update(gt);
			}
        }

        public override void OnStart()
        {
            em.AddEntity(player);
            lm.LoadLevel(new Level1(sb, em));
        }
        
        public override void OnExit()
        {
            Console.WriteLine("Rip");
        }

        private Color CoolFpsColorThing(float fps)
        {
            if (fps > 50.0f) { return new Color(0,255,0); }
            if (fps > 30.0f) { return Color.Orange;  }
            return Color.Red;
        }
    }  
}

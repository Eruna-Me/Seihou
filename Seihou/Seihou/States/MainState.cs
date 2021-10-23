using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

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

        bool pause = false;
        bool death = false;

        //Constructor
        public MainState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
            BuildMenus();
            lm = new LevelManager(em, sb);
            font1 = ResourceManager.fonts["DefaultFont"];
            player = new LenovoDenovoMan(sb, em, this.sm, this);

            MediaPlayer.Play(ResourceManager.songs["TestSong"]);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.05f;

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
				CloudManager.Instance.Update(gt);
            }

            if (pause) UpdatePauseMenu(gt);
            if (death) UpdateDeathMenu(gt);
			Cursor.Moved();
			Button.ButtonKeyControl(gt);
		}

        public void OnPlayerDeath()
        {
            death = true;
            if (Keyboard.GetState().IsKeyDown(Settings.GetKey("shootKey")))
            {
                Button.AwaitFireKeyUp = true;
            };
        }

        //When the state starts
        public override void OnStart()
        {
            lm.LoadLevel("Basiclevel");
            CloudManager.Initialize(sb, em);
            em.AddEntity(player);
        }

        //When the state exits
        public override void OnExit()
        {
            MediaPlayer.Stop();
            Debugging.Write(this,"Rip");
        }
    }
}
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
        private readonly EntityManager _entityManager = new();
        private readonly EntityFactory _entityFactory;
        private readonly LevelManager _levelManager;

        SpriteFont font1;
        Player player;
        KeyboardState oldKeyState;

        bool pause = false;
        bool death = false;

        //Constructor
        public MainState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
            BuildMenus();
            _entityFactory = new EntityFactory(_entityManager);
            _levelManager = new LevelManager(_entityFactory);
            SetupLevelDependencies();

            font1 = ResourceManager.fonts["DefaultFont"];

            MediaPlayer.Play(ResourceManager.songs["TestSong"]);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.05f;

            player = new LenovoDenovoMan(sb, _entityManager, this.sm, this);
            Global.player = player;
        }

        private void SetupLevelDependencies()
        {
            _entityFactory.AddDependency(_levelManager);
            _entityFactory.AddDependency(_entityManager);
            _entityFactory.AddDependency(sb);
            _entityFactory.AddDependency(gdm);
            _entityFactory.AddDependency(sm);
		}

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.FillRectangle(sb, new Vector2(0, 0), new Vector2(Global.playingFieldWidth, Global.screenHeight),Global.gameBackgroundColor, 0);
            _entityManager.Draw(gt);
            UI.Draw(gt, sb, sm, _entityManager);

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
                _levelManager.Update(gt);
                _entityManager.Update(gt);
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
            CloudManager.Initialize(sb, _entityManager);
            _levelManager.LoadLevel("Basiclevel");
            _entityManager.AddEntity(player);
            _entityFactory.Index();
        }

        //When the state exits
        public override void OnExit()
        {
            MediaPlayer.Stop();
            Debugging.Write(this,"Rip");
        }
    }
}
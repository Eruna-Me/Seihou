using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Seihou
{
    internal class MainState : State
    {
        private enum Status
        {
            none = -1,
            pause = 0,
            death = 1,
            gameEnd = 2,
        }

        private const int buttonSpacing = 30;
        private const float keyboardWaitTime = 1f;
        private float keyboardWaitTimer = 0;

        private readonly List<FormHost> menus = new()
        {
            new FormHost(),
            new FormHost(),
            new FormHost(),
        };

        private Status currentStatus = Status.none;

        //Variables
        private readonly EntityManager _entityManager = new();
        private readonly EntityFactory _entityFactory;
        private readonly LevelManager _levelManager;

        SpriteFont font1;
        readonly Player player;
        KeyboardState oldKeyState;

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
            UI.Draw(gt, sb, sm, _entityManager, _levelManager);

            DrawMenu(gt);
        }

        public override void Update(GameTime gt)
        {
            KeyboardState currentKeyState = Keyboard.GetState();

            if ((currentKeyState.IsKeyDown(Global.PauseKey1) && oldKeyState.IsKeyUp(Global.PauseKey1)) || (oldKeyState.IsKeyUp(Global.PauseKey2) && currentKeyState.IsKeyDown(Global.PauseKey2)))
            {
                if (currentStatus == Status.none)
                    SetStatus(Status.pause);
            }

            oldKeyState = currentKeyState;

            if (currentStatus == Status.none)
            {
                _levelManager.Update(gt);
                _entityManager.Update(gt);
				CloudManager.Instance.Update(gt);
            }

            UpdateMenu(gt);
		}

        public void OnPlayerDeath() => SetStatus(Status.death);

        public void OnGameOver() => SetStatus(Status.gameEnd);

        //When the state starts
        public override void OnStart()
        {
            CloudManager.Initialize(sb, _entityManager);
            _levelManager.LoadLevel("Level 1");
            _entityManager.AddEntity(player);
            _entityFactory.Index();
        }

        //When the state exits
        public override void OnExit()
        {
            MediaPlayer.Stop();
            Debugging.Write(this,"Rip");
        }

        //- Menu logic -

        private FormHost GetHost(Status menu)
        {
            if (menu == Status.none)
                return null;

            return menus[(int)menu];
        }

        private void UpdateMenu(GameTime gt)
        {
            var host = GetHost(currentStatus);
            if (host is not null)
            {
                host.Update(gt);
                host.BlockKeyboardUserInput = keyboardWaitTimer > 0;
                keyboardWaitTimer -= gt.Time();
            }
        }

        private void DrawMenu(GameTime gt)
        {
            if (currentStatus != Status.none)
                MonoGame.Primitives2D.FillRectangle(sb, new Vector2(0, 0), new Vector2(Global.screenWidth, Global.screenHeight), new Color(Color.Black, 0.6f), 0);

            GetHost(currentStatus)?.Draw(gt);
        }

        private void BuildMenus()
        {
            int width = 500;

            GetHost(Status.death).AddControl(new Button(sb, OnClickedContinue) { Text = "Continue", TabIndex = 0, Position = new Vector2(Global.Center.X, Global.Center.Y - buttonSpacing * 2) });
            GetHost(Status.death).AddControl(new Button(sb, OnClickedMenuScore) { Text = "Menu/Save score", TabIndex = 1, Position = new Vector2(Global.Center.X, Global.Center.Y) });
            GetHost(Status.death).AddControl(new Button(sb, OnClickedExit) { Text = "Exit", TabIndex = 2, Position = new Vector2(Global.Center.X, Global.Center.Y + buttonSpacing * 2) });

            GetHost(Status.pause).AddControl(new Button(sb, OnClickedResume) { Text = "Resume", TabIndex = 0, Position = new Vector2(Global.Center.X, Global.Center.Y - buttonSpacing * 2) });
            GetHost(Status.pause).AddControl(new Button(sb, OnClickedMenuScore) { Text = "Menu/Save score", TabIndex = 1, Position = new Vector2(Global.Center.X, Global.Center.Y) });
            GetHost(Status.pause).AddControl(new Button(sb, OnClickedExit) { Text = "Exit", TabIndex = 2, Position = new Vector2(Global.Center.X, Global.Center.Y + buttonSpacing * 2) });

            GetHost(Status.gameEnd).AddControl(new Button(sb, OnClickedMenuScore) { Text = "Menu/Save score", TabIndex = 0, Position = new Vector2(Global.Center.X, Global.Center.Y) });
            GetHost(Status.gameEnd).AddControl(new Button(sb, OnClickedExit) { Text = "Exit", TabIndex = 1, Position = new Vector2(Global.Center.X, Global.Center.Y + buttonSpacing * 2) });

            int tabIndex = 0;
            foreach (var control in menus.SelectMany(m => m.Controls))
            {
                var button = control as Button;
                button.Size = new Vector2(width, buttonSpacing);
                button.TabIndex = tabIndex++;
            }
        }

        private void SetStatus(Status status)
        {
            currentStatus = status;

            GetHost(currentStatus)?.ResetTabIndex();
            keyboardWaitTimer = keyboardWaitTime;
        }

        private void OnClickedContinue()
        {
            Global.player.Continue();
            currentStatus = Status.none;
        }

        private void OnClickedResume() => currentStatus = Status.none;

        private void OnClickedExit() => sm.abort = true;

        private void OnClickedMenuScore() => sm.ChangeState(new QuestionState(sm, cm, sb, gdm, Math.Round(Global.player.score), Settings.GetDifficulty()));
    }
}
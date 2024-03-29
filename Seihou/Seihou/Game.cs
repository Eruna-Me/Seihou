﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Seihou
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        StateManager stateManager;

        public Game()
        {
            this.IsMouseVisible = true;

            //Load settings
            Settings.ImportSettings();

            //Graphics options
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Global.screenWidth,
                PreferredBackBufferHeight = Global.screenHeight,
                SynchronizeWithVerticalRetrace = true
            };
            graphics.ApplyChanges();
            IsFixedTimeStep = false;

            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            //Set content directory
            Content.RootDirectory = "Content";

            stateManager = new StateManager();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ResourceManager.Load(Content);

            stateManager.ChangeState(new MenuState(stateManager, Content, spriteBatch, graphics));
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F11) || (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.Enter)))
            {
                graphics.ToggleFullScreen();
                graphics.SynchronizeWithVerticalRetrace = true;
                graphics.ApplyChanges();
            }

            DeveloperGameTime(ref gameTime);
            stateManager.Update(gameTime);

            if (stateManager.abort) Exit();

            base.Update(gameTime);
        }

        [Conditional("DEBUG")]
        private static void DeveloperGameTime(ref GameTime time)
        {
            time.ElapsedGameTime *= Keyboard.GetState().IsKeyDown(Keys.F2) ? 30 : 1;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate);
            stateManager.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

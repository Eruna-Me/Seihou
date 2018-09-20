using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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

            //Graphics options
            graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = Global.screenWidth;
			graphics.PreferredBackBufferHeight = Global.screenHeight;
            graphics.SynchronizeWithVerticalRetrace = true;
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

            //TODO: change testState to menustate
            stateManager.ChangeState(new TestState(stateManager, Content, spriteBatch, graphics)); //DoNT mOvE ThiS tO iNiTiAlIzE
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
			if (Keyboard.GetState().IsKeyDown(Keys.F11) || (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.Enter)))
			{
				graphics.ToggleFullScreen();
				graphics.SynchronizeWithVerticalRetrace = true;
				graphics.ApplyChanges();
			}

            stateManager.Update(gameTime);

			base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            stateManager.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

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
        readonly EntityManager entityManager;
        Player player;

        public Game()
        {
			this.IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = Global.screenWidth;
			graphics.PreferredBackBufferHeight = Global.screenHeight;
			graphics.ApplyChanges();
			Content.RootDirectory = "Content";

            entityManager = new EntityManager();
        }
		
        protected override void Initialize()
        {

  
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(new Vector2(300,300), spriteBatch, entityManager);
            entityManager.AddEntity(player);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            entityManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            entityManager.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

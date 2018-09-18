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
        private SpriteFont font;

        public Game()
        {
			this.IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = Global.screenWidth;
			graphics.PreferredBackBufferHeight = Global.screenHeight;
			graphics.ApplyChanges();
			Content.RootDirectory = "Content";
            font = Content.Load<SpriteFont>("DefaultFont");
            entityManager = new EntityManager();
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            SpriteManager.Load(Content);
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

            for (int i = 0; i < 50; i++)
            {

                Faller testEnemy = new Faller(new Vector2(300, i*10), spriteBatch, entityManager,(byte)(i/10));
                //TestEnemy testEnemy = new TestEnemy(new Vector2(300, 300), spriteBatch, entityManager);
                entityManager.AddEntity(testEnemy);
            }

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

			UI.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(font, $"FPS: {1 / gameTime.ElapsedGameTime.TotalSeconds} \nENTITIES: {entityManager.GetEntityCount()} \nREMOVE {entityManager.GetPollRemoveEntityCount()}", new Vector2(20, 20), Color.Green);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

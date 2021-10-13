using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	class CloudManager
	{
		public static CloudManager Instance { get; private set; }

		private const int AMOUNT_CLOUD_TEXTURES = 4;
		private const float SPEED = 50.0f;
		private const float MIN_ALPHA = 0.0f;
		private const float MAX_ALPHA = 0.8f;
		private const float SPEED_VARIANCE = 100.0f;
		private const float SPAWN_INTERVAL = 0.2f;

		private readonly SpriteBatch _spriteBatch;
		private readonly EntityManager _entityManager;

		private float _spawnTimer = 0;

		public static void Initialize(SpriteBatch sb, EntityManager em)
		{
			Instance = new CloudManager(sb, em);
			Instance.FillScreen();
		}

		private CloudManager(SpriteBatch spriteBatch, EntityManager entityManager)
		{
			_spriteBatch = spriteBatch;
			_entityManager = entityManager;
		}

		public void Update(GameTime gt)
		{	
			while (_spawnTimer > SPAWN_INTERVAL)
			{
				_spawnTimer -= _spawnTimer;
				SpawnCloud(-Cloud.SPAWN_MARGIN);
			}

			_spawnTimer += gt.Time();
		}

		public void FillScreen()
		{
			float y = -Cloud.SPAWN_MARGIN;
			while (y < Global.screenHeight + Cloud.SPAWN_MARGIN)
			{
				y += SPAWN_INTERVAL * GetSpeed();
				SpawnCloud(y);
			}
		}

		private void SpawnCloud(float y)
		{
			float x = Global.random.Next(-Global.outOfScreenMargin, Global.playingFieldWidth + Global.outOfScreenMargin);
			float alpha = (float)Global.random.NextDouble() * (MAX_ALPHA - MIN_ALPHA) - MIN_ALPHA;
			float speed = GetSpeed();
			bool mirror = Global.random.NextDouble() > .5;

			_entityManager.AddEntity(new Cloud(new Vector2(x, y), _spriteBatch, _entityManager, RandomCloudTexture(), alpha, speed, mirror));
		}

		private static float GetSpeed()
		{
			return SPEED + ((float)Global.random.NextDouble() * SPEED_VARIANCE);
		}

		private static string RandomCloudTexture()
		{
			return $"Cloud{Global.random.Next(1, AMOUNT_CLOUD_TEXTURES + 1)}";
		}
	}
}

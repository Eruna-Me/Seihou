using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Seihou
{
    internal class LevelManager
    {
        public Vector2 PlayerSpawn { get; private set; }

        public float CurrentHeight { get; private set; } = -1f;
        public float SpawnAtY { get; set; } = -100.0f;
        public float LevelSpeed { get; set; } = 40.0f;
        public bool Paused { get; set; } = false;

        private Queue<SpawnTask> _tasks;

        private readonly EntityFactory _factory;
        private readonly EntityManager _entityManager;

		public LevelManager(EntityManager entityManager, SpriteBatch spriteBatch)
        {
            _factory = new EntityFactory(spriteBatch, entityManager);
            _entityManager = entityManager;
		}

        public void LoadLevel(string name)
        {
            var level = File.ReadAllText(Path.Join("Content", "Levels", name + ".json"));
            var objects = ParseObjects(level);
            LoadObjects(objects);
        }

        private static JArray ParseObjects(string json)
        {
            var jobj = JObject.Parse(json);
            var layer = jobj["layers"]
                .Children()
                .First(l => (string)l["type"] == "objectgroup");

            return (JArray)layer["objects"];
        }

        private void LoadObjects(JArray objects)
        {
            List<SpawnTask> tasks = new();
            foreach(JObject obj in objects)
            {
                var type = (string)obj["type"];
                switch (type)
                {
                    case "Spawner":
                        tasks.Add(ParseSpawner(obj));
                        break;

                    //Ignore types used only in editor
                    case "Screen":
                    case "Barrier":
                        break;

                    default:
                        throw new JsonReaderException($"Unknown type '{type}'");
                }
			}

            _tasks = new Queue<SpawnTask>(tasks.OrderByDescending(t => t.WorldPosition.Y));
            _factory.Index();
        }

        private static SpawnTask ParseSpawner(JObject spawner)
        {
            var properties = spawner["properties"].ToDictionary(k => (string)k["name"], v => v["value"]);

            return new SpawnTask
            {
                EntityName = (string)properties["name"],
                WorldPosition = new Vector2(
                    (float)spawner["x"],
                    (float)spawner["y"]
                ),
                Size = new Vector2(
                    (float)spawner["width"],
                    (float)spawner["height"]
                )
            };
		}
        
        public void Update(GameTime gt)
        {
            CurrentHeight -= LevelSpeed * (float)gt.ElapsedGameTime.TotalSeconds;

            while (_tasks.Any() && _tasks.Peek().WorldPosition.Y > CurrentHeight)
            {
                var task = _tasks.Dequeue();
                var spawnAt = new Vector2(task.CenterWorld.X, SpawnAtY);
                var entity = _factory.Create(spawnAt, task.EntityName);
                _entityManager.AddEntity(entity);
			}
        }
    }
}

using System.Collections.Generic;
using System.Diagnostics;
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
        private record SpawnTask
        {
            public string EntityName { get; init; }
            public IReadOnlyDictionary<string,string> Properties { get; init; }

            public Vector2 MapPosition { get; init; }
            public Vector2 MapPositionCenter => MapPosition + Size / 2;
            public Vector2 Size { get; init; }
        }

        public string CurrentLevelName { get; private set; }
        public Vector2 PlayerSpawn { get; private set; }

        public float CurrentHeight { get; private set; } = -1f;
        public float SpawnAtY { get; set; } = -100.0f;
        public float LevelSpeed { get; set; } = 40.0f;

        private Queue<object> _pauseRequests = new();
        private bool _paused = false;
        private Queue<SpawnTask> _tasks;
        private readonly EntityFactory _factory;

		public LevelManager(EntityFactory factory)
        {
            _factory = factory;
		}

        public void Unpause(object by)
        {
            if (_pauseRequests.Peek() == by)
            {
                _pauseRequests.Dequeue();
                _paused = _pauseRequests.Any();
			}
		}

        public void Pause(object by)
        {
            _pauseRequests.Enqueue(by);
            _paused = true;
		}

        public void LoadLevel(string name)
        {
            CurrentLevelName = name;
            var level = File.ReadAllText(Path.Join("Content", "Levels", name + ".json"));
            var objects = ParseObjects(level);
            LoadObjects(objects);
        }

        private static JArray ParseObjects(string json)
        {
            var jobj = JObject.Parse(json);
            var layer = jobj["layers"]
                .Children()
                .First(l => (string)l["type"] == "objectgroup" && (string)l["name"] == "Enemies");

            return (JArray)layer["objects"];
        }

        private void LoadObjects(JArray objects)
        {
            List<SpawnTask> tasks = new();
            foreach(JObject obj in objects)
            {
                tasks.Add(ParseObject(obj));
			}

            _tasks = new Queue<SpawnTask>(tasks.OrderByDescending(t => t.MapPosition.Y).ThenBy(t => t.MapPosition.X));
            _factory.Index();
        }

        private static SpawnTask ParseObject(JObject obj)
        {
            IReadOnlyDictionary<string, string> properties = obj.ContainsKey("properties") ?
                obj["properties"].ToDictionary(k => (string)k["name"], v => (string)v["value"]) :
                new Dictionary<string, string>();

            Debug.Assert(obj.ContainsKey("type"), $"Object without a type in map ({obj})");

            return new SpawnTask
            {
                EntityName = (string)obj["type"],
                MapPosition = new Vector2((float)obj["x"], (float)obj["y"]),
                Size = new Vector2((float)obj["width"], (float)obj["height"]),
                Properties = properties, 
            };
		}
        
        public void Update(GameTime gt)
        {
            if (!_paused)
            {
                CurrentHeight -= LevelSpeed * gt.Time();

                while (_tasks.Any() && _tasks.Peek().MapPosition.Y > CurrentHeight)
                {
                    var task = _tasks.Dequeue();
                    var spawnAt = new Vector2(task.MapPositionCenter.X, SpawnAtY);
                    _factory.Spawn(new EntityCreationData
                    {
                        EntityName = task.EntityName,
                        Position = spawnAt,
                        Properties = task.Properties,
                    });

                    if (_paused)
                    {
                        break;
					}
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
    class SpawnTask
    {
        public bool waitUntilClear = false;
        public readonly float sleep = 0;
        public readonly Entity spawn;

        public SpawnTask(Entity spawn, float sleep,bool waitUntilClear)
        {
            this.waitUntilClear = waitUntilClear;
            this.sleep = sleep;
            this.spawn = spawn;
        }
    }

    abstract class Level
    {
        SpriteBatch sb;
        EntityManager em;

        protected static Vector2 GetSpawn(float location) => new Vector2(Global.playingFieldWidth * (location / 100.0f), Global.spawnHeight);

        public Level(SpriteBatch sb, EntityManager em)
        {
            this.sb = sb;
            this.em = em;
        }

        public readonly Queue<SpawnTask> spawner = new Queue<SpawnTask>();

        protected void Spawn(Entity e) => spawner.Enqueue(new SpawnTask(e, 0,false));
        protected void Spawn(Entity e, float t) => spawner.Enqueue(new SpawnTask(e, t,false));
        protected void Sleep(float t) => spawner.Enqueue(new SpawnTask(null,t,false));
        protected void WaitUntilClear() => spawner.Enqueue(new SpawnTask(null, 0, true));
    }

    class LevelManager
    {
        public bool waitUntilClear = false;
        private Queue<SpawnTask> spawner = new Queue<SpawnTask>();
        private EntityManager em;
        private float timer = 0.0f;
        public bool pause = false;

        public LevelManager(EntityManager em)
        {
            this.em = em;
        }

        public void LoadLevel(Level l)
        {
            spawner = new Queue<SpawnTask>(l.spawner);
        }

        public void Update(GameTime gt)
        {
            if (waitUntilClear)
            {
                waitUntilClear = em.GetEntityCount(EntityManager.EntityClass.enemy) != 0;
                if (!waitUntilClear)
                {
                    Debugging.Write(this, "Field cleared!");
                }

                return;
            }

            if (pause) return;

			if (MessageBox.waitForButtonPress) return;

			while (timer <= gt.ElapsedGameTime.TotalSeconds)
            {
                if (spawner.Count < 1) return;

                SpawnTask st = spawner.Dequeue();

                
                timer += st.sleep;
                if (st.spawn != null)
                {
                    Debugging.Write(this, $"Spawning {st.spawn}...");
                    em.AddEntity(st.spawn);
                }

                if (st.waitUntilClear)
                {
                    Debugging.Write(this, "Waiting until clear...");
                    waitUntilClear = st.waitUntilClear;
                    return;
                }

            }
            timer -= (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}

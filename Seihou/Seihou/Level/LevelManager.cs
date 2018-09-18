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

    class Level
    {
        public Level(byte[] data) => this.data = new Queue<byte>(data);

        //Getters
        public byte GetByte() => data.Dequeue();
        public short GetShort() => BitConverter.ToInt16(new byte[]{ data.Dequeue(), data.Dequeue() }, 0);

        private Queue<byte> data;
        public bool Empty() => (data.Count < 1);
    }

    class LevelManager
    {
        //Data
        private readonly EntityManager em;
        private readonly SpriteBatch sb;

        //Callback
        public delegate void LevelDelegate();
        public LevelDelegate OnLevelEnd;

        //Variables
        public bool paused = false;
        private Level currentLevel = null;
        private float timer = 0.0f;

        //Commands
        private enum Command { sleep, spawn, }


        //Constructor
        public LevelManager(EntityManager em,SpriteBatch sb)
        {
            this.sb = sb;
            this.em = em;
        }

        //Add new entities here
        private void SpawnEntity(short entityType,byte location,byte arguments)
        {
            Vector2 getLocation() => new Vector2(Global.playingFieldWidth * (location / byte.MaxValue), Global.spawnHeight);

            switch(entityType)
            {
                case 0: em.AddEntity(new TestEnemy(getLocation(), sb, em)); break;
                case 1: em.AddEntity(new Faller(getLocation(), sb, em)); break;
            }
        }

        //Load level
        private Level LoadLevel(string file)
        {
            return new Level(File.ReadAllBytes(file));
        }

        private void Start(Level l)
        {
            currentLevel = l;
            timer = 0.0f;
            paused = false;
        }

        private void Update(GameTime gt, EntityManager em, SpriteBatch sb)
        {
            if (timer > 0.0f)
            {
                timer -= (float)gt.ElapsedGameTime.TotalSeconds;
                return;
            }

            if (currentLevel != null && !paused)
            {
                if (currentLevel.Empty())
                {
                    currentLevel = null;
                    OnLevelEnd();
                    return;
                }
                   
                Command c = (Command)currentLevel.GetByte();

                switch (c)
                {
                    case Command.sleep:
                        timer = currentLevel.GetShort();
                        break;

                    case Command.spawn:
                        SpawnEntity(currentLevel.GetShort(), currentLevel.GetByte(), currentLevel.GetByte());
                        break;
                }
            }
        }
    }
}

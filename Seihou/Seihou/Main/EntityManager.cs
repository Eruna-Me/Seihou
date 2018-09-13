using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
    class EntityManager
    {
        private readonly List<Entity> entities = new List<Entity>();
		private readonly Queue<Entity> pollAddEntities = new Queue<Entity>();
		private readonly Queue<Entity> pollRemoveEntities = new Queue<Entity>();

		public EntityManager()
        {
        }

		public void RemoveEntity(Entity ent)
		{/*
			foreach (Entity i in entities)
			{
				foreach (Entity j in pollRemoveEntities)
				if (i.id == j.id)
					pollRemoveEntities.Dequeue(i);
			}
			*/
		}

        public void AddEntity(Entity ent)
        {
			pollAddEntities.Enqueue(ent);
        }

        public void Update(GameTime gt)
        {
            foreach (Entity e in entities) e.Update(gt);
			while(pollAddEntities.Count > 0)
			{
				entities.Add(pollAddEntities.Dequeue());
			}


        }

        public void Draw(GameTime gt)
        {
            foreach (Entity e in entities) e.Draw(gt);
        }

        public void ClearEntities() => entities.Clear(); 
    }
}

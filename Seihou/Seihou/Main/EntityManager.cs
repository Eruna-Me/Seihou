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
        public enum EntityClass
        {
            nonSolid = 0,
            enemy,
            player,
            ui,
        }

        private readonly Dictionary<EntityClass, List<Entity>>  entities =           new Dictionary<EntityClass, List<Entity>>();
        private readonly Dictionary<EntityClass, Queue<Entity>> pollAddEntities =    new Dictionary<EntityClass, Queue<Entity>>();
        private readonly Dictionary<EntityClass, Queue<Entity>> pollRemoveEntities = new Dictionary<EntityClass, Queue<Entity>>();

        public EntityManager()
        {
            entities.Add(EntityClass.nonSolid,           new List<Entity>());
            entities.Add(EntityClass.enemy,              new List<Entity>());
            entities.Add(EntityClass.player,             new List<Entity>());
            entities.Add(EntityClass.ui,                 new List<Entity>());

            pollAddEntities.Add(EntityClass.nonSolid,    new Queue<Entity>());
            pollAddEntities.Add(EntityClass.enemy,       new Queue<Entity>());
            pollAddEntities.Add(EntityClass.player,      new Queue<Entity>());
            pollAddEntities.Add(EntityClass.ui,          new Queue<Entity>());

            pollRemoveEntities.Add(EntityClass.nonSolid, new Queue<Entity>());
            pollRemoveEntities.Add(EntityClass.enemy,    new Queue<Entity>());
            pollRemoveEntities.Add(EntityClass.player,   new Queue<Entity>());
            pollRemoveEntities.Add(EntityClass.ui,       new Queue<Entity>());
        }
    
        public Entity GetPlayer() => entities[EntityClass.player][0];

        public Entity FindById(int id)
        {
            foreach (KeyValuePair<EntityClass, List<Entity>> pair in entities)
            {
                foreach (Entity e in pair.Value)
                {
                    if (e.id == id)
                    {
                        return e;
                    }
                }
            }
            return null;
        }

        public Entity Touching(Entity ent,EntityClass ec)
        {
            foreach (Entity e in entities[ec])
            {
                if (e.id == ent.id) continue;
                if (Collision.Circle(ent,e))
                {
                    return e;
                }
            }
            return null;
        }

        public Entity Touching(Vector2 pos, int size, EntityClass ec)
        {
            foreach (Entity e in entities[ec])
            {
                if (Collision.Circle(e,pos,size))
                {
                    return e;
                }
            }
            return null;
        }


        public void DamageAll(int damage,EntityClass ec)
        {
            foreach (Entity e in entities[ec])
            {
                e.OnDamaged(null, damage);
            }
        }

        public int GetEntityCount()
        {
            int c = 0;
            foreach (KeyValuePair<EntityClass, List<Entity>> pair in entities)
                c += pair.Value.Count;
            return c;
        }

        public int GetEntityCount(EntityClass e) => entities[e].Count;

        public void RemoveEntity(Entity ent) => pollRemoveEntities[ent.ec].Enqueue(ent);

        public void AddEntity(Entity ent) => pollAddEntities[ent.ec].Enqueue(ent);

        public void Update(GameTime gt)
        {
            foreach (KeyValuePair<EntityClass, List<Entity>> pair in entities)
            {
                foreach (Entity e in pair.Value) e.Update(gt);
            }

            foreach (KeyValuePair<EntityClass, Queue<Entity>> pair in pollAddEntities)
            {
                while (pair.Value.Count > 0)
                    entities[pair.Key].Add(pair.Value.Dequeue());
            }

            foreach (KeyValuePair<EntityClass, Queue<Entity>> pair in pollRemoveEntities)
            {
                while (pair.Value.Count > 0)
                    entities[pair.Key].Remove(pair.Value.Dequeue());
            }
        }

        public void Draw(GameTime gt)
        {
            foreach (KeyValuePair<EntityClass, List<Entity>> pair in entities)
            {
                foreach (Entity e in pair.Value)
                    e.Draw(gt);
            }
        }

        public void ClearEntities()
        {
            foreach (KeyValuePair<EntityClass, List<Entity>> pair in entities)
                pair.Value.Clear();
        }
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Seihou
{
    public class EntityManager
    {
        public enum EntityClass
        {
			cloud,
            nonSolid,
			enemyProjectile,
			enemy,
            player,
            ui,
        }

		public enum Collections
		{
			Entities,
			PollAdd,
			PollRemove,
		}

		public Dictionary<Collections, Dictionary<EntityClass,List<Entity>>> EntityCollections = new Dictionary<Collections, Dictionary<EntityClass, List<Entity>>>()
		{
			{Collections.Entities, new Dictionary<EntityClass, List<Entity>>() },
			{Collections.PollAdd, new Dictionary<EntityClass, List<Entity>>() },
			{Collections.PollRemove, new Dictionary<EntityClass, List<Entity>>() },
		};

        public EntityManager()
        {
			foreach (var i in EntityCollections.Values) foreach (var p in System.Enum.GetValues(typeof(EntityClass))) i.Add((EntityClass)p,new List<Entity>());
		}

		public Entity GetPlayer() => EntityCollections[Collections.Entities][EntityClass.player][0];

        public Entity FindById(int id)
        {
            foreach (KeyValuePair<EntityClass, List<Entity>> pair in EntityCollections[Collections.Entities])
            {
                foreach (Entity e in pair.Value)
					if (e.id == id) return e;
            }
            return null;
        }

        public Entity Touching(Entity ent,EntityClass ec)
        {
            foreach (Entity e in EntityCollections[Collections.Entities][ec])
            {
                if (e.id == ent.id) continue;
                if (Collision.Circle(ent,e)) return e;
            }
            return null;
        }

		public Entity Touching(Entity ent, EntityClass ec, Entity lastEntity)
		{
			bool alreadySearched = true;

			if (lastEntity == null) alreadySearched = false;

			for ( int i = 0; i < EntityCollections[Collections.Entities][ec].Count; i++)
			{
				if (!alreadySearched)
				{
					if (EntityCollections[Collections.Entities][ec][i].id == ent.id) continue;
					if (Collision.Circle(ent, EntityCollections[Collections.Entities][ec][i])) return EntityCollections[Collections.Entities][ec][i];
				}
				else if (EntityCollections[Collections.Entities][ec][i].id == lastEntity.id && alreadySearched)
				{
					alreadySearched = false;
				}
			}
			return null;
		}

		public Entity Touching(Vector2 pos, int size, EntityClass ec)
        {
            foreach (Entity e in EntityCollections[Collections.Entities][ec])
                if (Collision.Circle(e,pos,size)) return e;
            return null;
        }

        public List<Entity> GetEntities(EntityClass e) => EntityCollections[Collections.Entities][e];

        public void DamageAll(int damage,EntityClass ec)
        {
            foreach (Entity e in EntityCollections[Collections.Entities][ec]) e.OnDamaged(null, damage);
        }

        public int GetEntityCount()
        {
            int c = 0;
            foreach (KeyValuePair<EntityClass, List<Entity>> pair in EntityCollections[Collections.Entities])
				c += pair.Value.Count;
            return c;
        }

        public int GetEntityCount(EntityClass e) => EntityCollections[Collections.Entities][e].Count;

		public void RemoveEntity(Entity ent) => EntityCollections[Collections.PollRemove][ent.ec].Add(ent);

		public void AddEntity(Entity ent) => EntityCollections[Collections.PollAdd][ent.ec].Add(ent);

        public void Update(GameTime gt)
        {
            foreach (KeyValuePair<EntityClass, List<Entity>> pair in EntityCollections[Collections.Entities])
                foreach (Entity e in pair.Value) e.Update(gt);

            foreach (KeyValuePair<EntityClass, List<Entity>> pair in EntityCollections[Collections.PollAdd])
            {
				while (pair.Value.Count > 0)
				{
					EntityCollections[Collections.Entities][pair.Key].Add(pair.Value.Last());
					EntityCollections[Collections.PollAdd][pair.Key].Remove(pair.Value.Last());
				}
            }

            foreach (KeyValuePair<EntityClass, List<Entity>> pair in EntityCollections[Collections.PollRemove])
            {
				while (pair.Value.Count > 0)
				{
					EntityCollections[Collections.Entities][pair.Key].Remove(pair.Value.Last());
					EntityCollections[Collections.PollRemove][pair.Key].Remove(pair.Value.Last());
				}
            }
        }

        public void Draw(GameTime gt)
        {
            foreach (KeyValuePair<EntityClass, List<Entity>> pair in EntityCollections[Collections.Entities])
            {
                foreach (Entity e in pair.Value)
                    e.Draw(gt);
            }
        }

        public void ClearEntities()
        {
            foreach (KeyValuePair<EntityClass, List<Entity>> pair in EntityCollections[Collections.Entities])
                pair.Value.Clear();
        }
    }
}
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

        public EntityManager()
        {
        }

        public void AddEntity(Entity ent)
        {
            entities.Add(ent);
        }

        public void Update(GameTime gt)
        {
            foreach (Entity e in entities) e.Update(gt);
        }

        public void Draw(GameTime gt)
        {
            foreach (Entity e in entities) e.Draw(gt);
        }

        public void ClearEntities() => entities.Clear(); 
    }
}

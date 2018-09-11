using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
    class EntityManager
    {
        private readonly List<Entity> entities = new List<Entity>();
        

        public void AddEntity(Entity ent)
        {
            entities.Add(ent);
        }


        public void ClearEntities() => entities.Clear();

        
    }
}

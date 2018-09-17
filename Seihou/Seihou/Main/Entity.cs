using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    abstract class Entity
    {
        private static int newId = 0;
        public readonly int id;
        protected SpriteBatch sb;
        protected Vector2 pos;
		protected EntityManager em;
        public Global.Faction faction = Global.Faction.noFaction;

        protected Entity(Vector2 pos, SpriteBatch sb, EntityManager em)
        {
            this.sb = sb;
            this.pos = pos;
			this.em = em;

            newId++;
            id = newId;
        }

        public abstract void Draw(GameTime gt);
        public abstract void Update(GameTime gt);
    }
}

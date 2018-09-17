using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    abstract class Entity
    {
        private static int newId = 0;
        public readonly int id;
        protected SpriteBatch sb;
        public Vector2 pos;
		protected EntityManager em;

        //Optional
        public Vector2 speed = new Vector2(0, 0);
        public Global.Faction faction = Global.Faction.noFaction;
        public int size = 1;

        public virtual void Damage(Entity by, int damage)
        {

        }

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

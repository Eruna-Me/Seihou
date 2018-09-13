using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    abstract class Entity
    {
        private static int newId = 0;
        public readonly int id;
        protected SpriteBatch sb;
        protected float x, y;
		protected EntityManager em;

        protected Entity(float x, float y, SpriteBatch sb, EntityManager em)
        {
            this.sb = sb;
            this.x = x;
            this.y = y;
			this.em = em;

            newId++;
            id = newId;
        }

        public abstract void Draw(GameTime gt);
        public abstract void Update(GameTime gt);
    }
}

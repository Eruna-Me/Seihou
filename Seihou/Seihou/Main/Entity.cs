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

        public Entity(int x, int y, SpriteBatch sb)
        {
            this.sb = sb;
            this.x = x;
            this.y = y;

            newId++;
            id = newId;
        }

        public abstract void Draw(GameTime gt);
        public abstract void Update(GameTime gt);
    }
}

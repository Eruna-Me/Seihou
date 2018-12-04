using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	abstract class Control
    {
		protected SpriteBatch sb;

		public Control(SpriteBatch sb)
		{
			this.sb = sb;
		}

		public Color background = Color.Transparent;
		public Color textColor = Color.White;

        public abstract void Draw(GameTime gt);
        public abstract void Update(GameTime gt);
    }
}

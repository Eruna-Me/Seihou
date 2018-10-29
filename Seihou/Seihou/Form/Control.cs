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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	internal abstract class Control
    {
		public int TabIndex { get; set; } = -1;

		protected SpriteBatch sb;

		public Control(SpriteBatch sb)
		{
			this.sb = sb;
		}

        public abstract void Draw(GameTime gt);
		public abstract void Update(GameTime gt);
		public virtual void MouseMove(Vector2 mouse) { }
		public virtual void SetTabIndex(int index) { }

		public virtual void Press() { }
		public virtual void Release() { }
		public virtual void OnKeyboardPress() { }
	}
}

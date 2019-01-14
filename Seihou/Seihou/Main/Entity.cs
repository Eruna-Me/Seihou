using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	public abstract class Entity
	{
		private static int newId = 0;
		public int hp = 1;
		public readonly int id;
		public SpriteBatch sb;
		public Vector2 pos;
		protected EntityManager em;
		public string texture;

		//Optional
		public Vector2 speed = new Vector2(0, 0);
		public int size = 1;
		public EntityManager.EntityClass ec = EntityManager.EntityClass.nonSolid;

		public virtual void OnDamaged(Entity by, int damage)
		{
			hp -= damage;
		}

		protected Entity(Vector2 pos, SpriteBatch sb, EntityManager em)
		{
			this.sb = sb;
			this.pos = pos;
			this.em = em;

			newId++;
			id = newId;
		}

		public virtual void Draw(GameTime gt)
		{
			sb.Draw(ResourceManager.textures[texture], pos - ResourceManager.Center(texture), Color.White);
			#if DRAWCOLBOX
			MonoGame.Primitives2D.DrawCircle(sb, pos, size, 10, Color.White, 1);
			#endif
		}
		public abstract void Update(GameTime gt);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	class LiveUp : Powerup
	{
		public LiveUp (Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
		{
			texture = "LiveUp";
		}

		public override void Update(GameTime gt)
		{
			base.Update(gt);

			Entity c = em.Touching(this, EntityManager.EntityClass.player);

			if (c != null && hp > 0)
			{
				hp--;
				em.RemoveEntity(this);
				Global.player.lives++;
			}
		}
	}
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Seihou
{
	class Cloud : Entity
	{
		private readonly float alpha;
		private readonly float deltaSpeed;

		public Cloud(Vector2 pos, SpriteBatch sb, EntityManager em, String texture, float alpha, float deltaSpeed) : base(pos, sb, em)
		{
			ec = EntityManager.EntityClass.cloud;
			this.alpha = alpha;
			this.deltaSpeed = deltaSpeed;
		}

		public override void Update(GameTime gt)
		{
			pos.Y += (CloudManager.speed + deltaSpeed) * gt.Time();

			if (pos.Y > Global.screenHeight + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}
		}

		public override void Draw(GameTime gt)
		{
			sb.Draw(ResourceManager.textures["Cloud1"], pos, new Color(CloudManager.color, alpha));
		}
	}
}

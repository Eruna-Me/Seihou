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
	class Cloud : Entity
	{
		private float alpha;
		private float deltaSpeed;

		public Cloud(Vector2 pos, SpriteBatch sb, EntityManager em, String texture, float alpha, float deltaSpeed) : base(pos, sb, em)
		{
			ec = EntityManager.EntityClass.cloud;
			this.alpha = alpha;
			this.deltaSpeed = deltaSpeed;
		}

		public override void Update(GameTime gt)
		{
			pos.Y += (CloudManager.speed + deltaSpeed) * (float)gt.ElapsedGameTime.TotalSeconds;

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

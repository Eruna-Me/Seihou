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

		public Cloud(Vector2 pos, SpriteBatch sb, EntityManager em, String texture, float alpha) : base(pos, sb, em)
		{
			ec = EntityManager.EntityClass.cloud;
			this.alpha = alpha;
		}

		public override void Update(GameTime gt)
		{
			pos += CloudManager.speed * (float)gt.ElapsedGameTime.TotalSeconds;
		}

		public override void Draw(GameTime gt)
		{
			sb.Draw(ResourceManager.textures["Cloud1"], pos, new Color(CloudManager.color, alpha));
		}
	}
}

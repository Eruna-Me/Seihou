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
	class Point : Entity
	{
		public Point(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
		{
			size = 20;
		}

		public override void Update(GameTime gt)
		{
			Entity c = em.Touching(this, EntityManager.EntityClass.player);

			if (c != null && hp > 0)
			{
				hp--;
				em.RemoveEntity(this);
				Global.player.CollectPoint();
			}
		}

		public override void Draw(GameTime gt)
		{
			sb.Draw(ResourceManager.textures["Point"], pos - ResourceManager.Origin("Point"), Color.White);
			MonoGame.Primitives2D.DrawCircle(sb, pos, size, 3, Color.Blue, 1);
		}	
	}
}

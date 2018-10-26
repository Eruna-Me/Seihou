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
	class PlayerBullet : PlayerProjectile
	{
		public PlayerBullet(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed, string texture) : base(pos, sb, em, owner)
		{
			this.texture = texture;
			ec = EntityManager.EntityClass.nonSolid;
			size = 3;
			this.speed = speed;
		}
	}
}

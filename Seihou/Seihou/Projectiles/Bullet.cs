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
	class Bullet : EnemyProjectile
	{
        //TODO: add owner here
		public Bullet(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em, owner)
		{
			texture = "Dart1";
			ec = EntityManager.EntityClass.nonSolid;
			size = 3;
			this.speed = speed;
		}
	}
}

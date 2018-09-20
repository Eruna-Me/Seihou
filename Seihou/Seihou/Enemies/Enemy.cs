using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Seihou
{
    abstract class Enemy : Entity 
    {
        protected Enemy(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
            ec = EntityManager.EntityClass.enemy;
        }
		public override void Update(GameTime gt)
		{
			if (pos.Y > Global.screenHeight + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}
		}
	}
}

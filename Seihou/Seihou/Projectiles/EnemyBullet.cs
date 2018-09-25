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
	class EnemyBullet : Projectile
	{

        private Trail trail;
		public EnemyBullet(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em, owner)
		{
            trail = new Trail(1,sb,ResourceManager.textures["EnemyBullet"]);
            ec = EntityManager.EntityClass.nonSolid;
			size = 3;
			this.speed = speed;
		}

		public override void Update(GameTime gt)
		{
            base.Update(gt);

            trail.AddSection(pos);

            pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

			EntityManager.EntityClass target = (owner is Enemy) ? EntityManager.EntityClass.player : EntityManager.EntityClass.enemy ;
			
			Entity c = em.Touching(this,target);

			if (c != null)
			{
				if (c.hp > 0 && this.hp > 0)
				{
					hp--;
					c.OnDamaged(owner, 1);
					em.RemoveEntity(this);
				}
			}
		}

		public override void Draw(GameTime gt)
		{
            trail.Draw(gt);

            sb.Draw(ResourceManager.textures["EnemyBullet"], pos, Color.White);
		}
	}
}

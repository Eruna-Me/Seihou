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
	class CoolBullet : Projectile
	{
		private Vector2 maxSpeed;
		public CoolBullet(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em, owner)
		{
            ec = EntityManager.EntityClass.nonSolid;
			size = 10;
			maxSpeed = speed;
			this.speed = speed / 5;
		}

		public override void Update(GameTime gt)
		{
			base.Update(gt);
			if ((speed.Length() < maxSpeed.Length()) || false) speed += maxSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
			pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

            EntityManager.EntityClass target;
            if (owner is Enemy)
            {
                target = EntityManager.EntityClass.player;
            }
            else
            {
                target = EntityManager.EntityClass.enemy;
            }

			Entity c = em.Touching(this,target);

			if (c != null)
			{
			    c.OnDamaged(owner, 1);
			}
		}

		public override void Draw(GameTime gt)
		{	
			ResourceManager.DrawAngledTexture(sb, "Dart1", pos, speed);
		}
	}
}

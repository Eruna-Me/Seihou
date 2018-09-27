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
	class CoolBullet : EnemyProjectile
	{
		private Vector2 maxSpeed;
		public CoolBullet(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em, owner)
		{
			texture = "Dart1";
			ec = EntityManager.EntityClass.nonSolid;
			size = 10;
			maxSpeed = speed;
			this.speed = speed / 5;
		}

		public override void Update(GameTime gt)
		{
			if ((speed.Length() < maxSpeed.Length()) || false) speed += maxSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
			pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

            Entity c = em.Touching(this, EntityManager.EntityClass.player);

			if (c != null)
			{
				if (c.hp > 0 && hp > 0)
				{
					hp--;
					c.OnDamaged(owner, 1);
					em.RemoveEntity(this);
				}
			}

			if (pos.Y + Global.outOfScreenMargin < 0 || pos.Y > Global.screenHeight + Global.outOfScreenMargin || pos.X + Global.outOfScreenMargin < 0 || pos.X > Global.playingFieldWidth + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}
		}
	}
}

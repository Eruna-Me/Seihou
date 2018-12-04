using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	abstract class EnemyProjectile : Entity
	{
		protected readonly Entity owner;

		protected EnemyProjectile(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner) : base(pos, sb, em)
		{ 
			ec = EntityManager.EntityClass.enemyProjectile;
            this.owner = owner;
		}

		public override void Update(GameTime gt)
		{
			pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

			Entity c = em.Touching(this, EntityManager.EntityClass.player);
			Entity g = em.Touching(pos, Global.player.grazeDistance, EntityManager.EntityClass.player);
			if (g != null)
			{
				if (c != null)
				{
					if (c.hp > 0 && hp > 0)
					{
						hp--;
						c.OnDamaged(owner, 1);
						em.RemoveEntity(this);
					}
				}
				else Global.player.Graze(gt);
			}

			if (pos.Y + Global.outOfScreenMargin < 0 || pos.Y > Global.screenHeight + Global.outOfScreenMargin || pos.X + Global.outOfScreenMargin < 0 || pos.X > Global.playingFieldWidth + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}
		}

		public override void OnDamaged(Entity by, int damage)
		{
			em.RemoveEntity(this);
			hp--;
		}

		public override void Draw(GameTime gt)
		{
			ResourceManager.DrawAngledTexture(sb, texture, pos, speed);
			#if DRAWCOLBOX 
			MonoGame.Primitives2D.DrawCircle(sb, pos, size, 10, Color.White, 1);
			#endif
		}
	}
}
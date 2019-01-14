using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	class FlowerBombShrapnel : Entity
	{
		Trail trail;
		float rotation = 0;
		const float bombScore = 100.0f;
		public FlowerBombShrapnel(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em)
		{
			texture = "EnergyBall";
			trail = new Trail(sb, texture,20,0.016f);
			this.speed = speed;
			size = ResourceManager.textures[texture].Height / 2;
		}

		public override void Update(GameTime gt)
		{
			pos += speed /4 * (float)gt.ElapsedGameTime.TotalSeconds;
			rotation += (float)gt.ElapsedGameTime.TotalSeconds;
			trail.Update(pos,gt, rotation);

			Entity lastCollidedEntity = null;

			CollisionCheckStart:;

			Entity c = em.Touching(this, EntityManager.EntityClass.enemy, lastCollidedEntity);

			if (c is Boss) c = null;
			if (c == null) c = em.Touching(this, EntityManager.EntityClass.enemyProjectile, lastCollidedEntity);

			if (c != null)
			{
				lastCollidedEntity = c;
				if (c.hp > 0)
				{
					c.OnDamaged(this, 10000);
					float scoreGain = bombScore;
					Global.player.score += scoreGain;
					em.AddEntity(new MessageBox(pos + new Vector2(0, -50), sb, em, scoreGain.ToString(), 2.5f, 0, 1, "DefaultFont", 1f) { color = Color.Blue });
				}
				goto CollisionCheckStart;
			}

			if (pos.Y + Global.outOfScreenMargin < 0 || pos.Y > Global.screenHeight + Global.outOfScreenMargin || pos.X + Global.outOfScreenMargin < 0 || pos.X > Global.playingFieldWidth + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}
		}

		public override void Draw(GameTime gt)
		{
			trail.Draw(gt);
			sb.Draw(ResourceManager.textures[texture], pos, null, Color.White, (rotation * (float)Math.PI), ResourceManager.Center(texture), 1.0f, SpriteEffects.None, 0f);
		}
	}
}

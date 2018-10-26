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
	class FlowerBombShrapnel : Entity
	{
		Trail trail;
		float rotation = 0;
		float bombScore = 100.0f;
		public FlowerBombShrapnel(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em)
		{
			texture = "EnergyBall";
			trail = new Trail(20, sb, texture);
			this.speed = speed;
			size = ResourceManager.textures[texture].Height / 2;
		}

		public override void Update(GameTime gt)
		{
			pos += speed /4 * (float)gt.ElapsedGameTime.TotalSeconds;
			rotation += (float)gt.ElapsedGameTime.TotalSeconds;
			trail.AddSection(pos, rotation);
			Bob:;
			Entity c = em.Touching(this, EntityManager.EntityClass.enemy);

			if (c is Boss) c = null;
			if (c == null) c = em.Touching(this, EntityManager.EntityClass.enemyProjectile);

			if (c != null && c.hp > 0)
			{
				c.OnDamaged(this, 10000);
				float scoreGain = bombScore;
				Global.player.score += scoreGain;
				em.AddEntity(new MessageBox(pos + new Vector2(0, -50), sb, em, scoreGain.ToString(), 2.5f, 0, 1, "DefaultFont", 1f) { color = Color.Blue });
				goto Bob;
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

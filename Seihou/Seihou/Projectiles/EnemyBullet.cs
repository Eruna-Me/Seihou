using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	class EnemyBullet : EnemyProjectile
	{

        private Trail trail;
		public EnemyBullet(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed, String texture = "EnemyBullet") : base(pos, sb, em, owner)
		{
			this.texture = texture;
            trail = new Trail(sb,texture,1,0.01f);
			size = texture.Length/2;
			this.speed = speed;
		}

		public override void Update(GameTime gt)
		{
            base.Update(gt);
            trail.Update(pos,gt);
		}

		public override void Draw(GameTime gt)
		{
			base.Draw(gt);
            trail.Draw(gt);
		}
	}
}

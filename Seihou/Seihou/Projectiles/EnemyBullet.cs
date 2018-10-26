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
	class EnemyBullet : EnemyProjectile
	{

        private Trail trail;
		public EnemyBullet(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed, String texture = "EnemyBullet") : base(pos, sb, em, owner)
		{
			this.texture = texture;
            trail = new Trail(1,sb,texture);
			size = 3;
			this.speed = speed;
		}

		public override void Update(GameTime gt)
		{
            base.Update(gt);
            trail.AddSection(pos);
		}

		public override void Draw(GameTime gt)
		{
			base.Draw(gt);
            trail.Draw(gt);
		}
	}
}

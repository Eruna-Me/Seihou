using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	class JSF : Enemy
	{
		private const float fallSpeed = 50.0f;
		private const float bulletSpeed = 400.0f;
		private float fireDelay = 1f;
		private const float maxFireDelay = 0.5f;


		public JSF(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
		{
			size = 10;
			speed.Y = 10f;
		}

		public override void Damage(Entity by, int damage)
		{
			for (int i = 0; i < 50; i++)
				em.AddEntity(new Particle(pos, sb, em));
			em.RemoveEntity(this);
		}

		public override void Draw(GameTime gt)
		{
			MonoGame.Primitives2D.DrawCircle(sb, pos, 10, 3, Color.Red, 3);
		}

		public override void Update(GameTime gt)
		{
			base.Update(gt);
			pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;
			if (fireDelay <= 0)
			{
				em.AddEntity(new CoolBullet(pos, sb, em, this, Global.Normalize(em.GetPlayer().pos - pos) * bulletSpeed));
				fireDelay = maxFireDelay;
			}

			fireDelay -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;
		}
	}
}

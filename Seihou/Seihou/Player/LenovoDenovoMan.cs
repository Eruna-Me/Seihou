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
	class LenovoDenovoMan : Player
	{
		private const int powerStage1 = 5;
		private const float bulletSpeed = 500.0f;
		private const float bulletSpread = 30.0f;

		public LenovoDenovoMan(SpriteBatch sb, EntityManager em, StateManager sm, State state) : base(sb, em, sm, state)
		{
			texture = "Lenovo-DenovoMan";
			fullPower =  20;
		}

		public override void Fire()
		{
			em.AddEntity(new PlayerBullet(pos, sb, em, this, new Vector2(0, -bulletSpeed)));
			if (power >= powerStage1)
			{
				em.AddEntity(new PlayerBullet(pos, sb, em, this, new Vector2(bulletSpread, -bulletSpeed)));
				em.AddEntity(new PlayerBullet(pos, sb, em, this, new Vector2(-bulletSpread, -bulletSpeed)));
			}
			if (power >= fullPower)
			{
				em.AddEntity(new PlayerBullet(pos, sb, em, this, new Vector2(-bulletSpread * 2, -bulletSpeed)));
				em.AddEntity(new PlayerBullet(pos, sb, em, this, new Vector2(bulletSpread * 2, -bulletSpeed)));
			}
		}
	}
}

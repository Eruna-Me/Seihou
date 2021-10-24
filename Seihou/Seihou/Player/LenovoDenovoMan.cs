using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	class LenovoDenovoMan : Player
	{
		private const int powerStage1 = 20;
		private const int powerStage2 = 60;
		private const float bulletSpeed = 500.0f;
		private int bulletsPerShot = 5;
		private const float baseBulletSpread = 40.0f;
		private const float preciseBulletSpread = 80.0f;
		private const string bulletTexture = "Dart1";
		private float spread;

		public LenovoDenovoMan(SpriteBatch sb, EntityManager em, StateManager sm, State state) : base(sb, em, sm, state)
		{
			texture = "Lenovo-DenovoMan";
			fullPower =  100;
		}

		public override void Fire(bool slowMode)
		{
			SoundHelper.PlayRandom("Shoot");
			spread = slowMode ? preciseBulletSpread : baseBulletSpread;

			float direction = (float)-Math.PI / 2;

			if (power >= fullPower) bulletsPerShot = 5;
			else if (power >= powerStage2) bulletsPerShot = 3;
			else if (power >= powerStage1) bulletsPerShot = 2;
			else bulletsPerShot = 1;

			SpreadShot(pos, sb, em, this, bulletSpeed, bulletTexture, direction, spread, bulletsPerShot);
		}

		public override void DropBomb()
		{
			int amount = 101;

			for (float i = 0; i < Math.PI * 2; i += (float)(Math.PI * 2 / (amount)))
			{
				Vector2 dir = new Vector2((float)Math.Cos(i), (float)Math.Sin(i)) * bulletSpeed;
				em.AddEntity(new FlowerBombShrapnel(pos, sb, em, this, dir));
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//standard faller
namespace Seihou
{
    class Kitsune : Enemy
    {
        private const float fallSpeed = 10.0f;
		private const float bulletSpeed = 100.0f;
		private float fireDelay = 1.5f;
		private float maxFireDelay = 0.5f;
		private int bulletsPerShot = 21;
		private const string bulletTexture = "EnemyBullet";
		private double direction = 0;
		private bool clockwiseRotation;

		public Kitsune(Vector2 pos, SpriteBatch sb, EntityManager em, bool clockwiseRotation = true) : base(pos, sb, em)
		{
			texture = "Kitsune";
			ec = EntityManager.EntityClass.enemy;
			size = 24;
			speed.Y = fallSpeed;
			hp = 30;
			this.clockwiseRotation = clockwiseRotation;
		}
        public override void Update(GameTime gt)
        {
            base.Update(gt);

            pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

			if (fireDelay <= 0 && Global.OnScreen(pos))
			{
				float spread = (float)bulletsPerShot / 2.0f;

				Global.SpreadShot(pos, sb, em, this, bulletSpeed, bulletTexture, (float)direction, spread, bulletsPerShot);

				fireDelay = maxFireDelay;
			}

			fireDelay -= (float)gt.ElapsedGameTime.TotalSeconds;
			if (clockwiseRotation)
			{
				direction += Math.PI / 20 * gt.ElapsedGameTime.TotalSeconds;
			}
			else
			{
				direction -= Math.PI / 20 * gt.ElapsedGameTime.TotalSeconds;
			}
		}
    }
}

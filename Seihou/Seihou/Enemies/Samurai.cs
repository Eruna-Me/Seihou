using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//standard faller
namespace Seihou
{
	class Samurai : Enemy
    {
        private const float fallSpeed = 90.0f;
		private const float bulletSpeed = 400.0f;
		private float fireDelay = 2.0f;
		private readonly float maxFireDelay = 2.0f;

		public Samurai(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
		{
			texture = "Samurai";
			ec = EntityManager.EntityClass.enemy;
			size = 24;
			speed.Y = fallSpeed;
			hp = 3;

			if (Settings.GetDifficulty() == Settings.Difficulty.usagi)
				maxFireDelay = 1.25f;
		}

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            pos += speed * gt.Time();

			if (fireDelay <= 0 && Settings.GetDifficulty() >= Settings.Difficulty.hard && Global.OnScreen(pos))
			{
				em.AddEntity(new EnemyBullet(pos, sb, em, this, Global.Normalize(em.GetPlayer().pos - pos) * bulletSpeed));
				fireDelay = maxFireDelay;
			}

			fireDelay -= 1 * gt.Time();
		}
    }
}

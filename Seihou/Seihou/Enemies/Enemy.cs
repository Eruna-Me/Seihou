using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Seihou
{
	abstract class Enemy : Entity 
    {
		protected int scoreDropChance = 50;
		protected int powerDropChance = 30;
        protected int explosionParticles = 5;
		protected int scoreOnKilled = 100;

        protected Enemy(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
            ec = EntityManager.EntityClass.enemy;
        }

        public override void OnDamaged(Entity by, int damage)
        {
			SoundHelper.PlayRandom("EnemyPain");
			hp -= damage ;

			if (hp <= 0)
			{
				SoundHelper.PlayRandom("ExplosionShort");

				int randomNumber = Global.random.Next(0, 100);

				for (int i = 0; i < explosionParticles; i++)
				{
					em.AddEntity(new Particle(pos, sb, em));
				}

				if (randomNumber < scoreDropChance + powerDropChance)
				{
					if (randomNumber < powerDropChance)
						em.AddEntity(new Power(pos, sb, em));
					else
						em.AddEntity(new Point(pos, sb, em));
				}
				
				em.RemoveEntity(this);
				Global.player.score += scoreOnKilled;
			}
        }

        public override void Update(GameTime gt)
		{
			if (pos.Y > Global.screenHeight + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}

			Entity c = em.Touching(this, EntityManager.EntityClass.player);

			if (c != null && hp > 0) c.OnDamaged(this, 1);
		}
	}
}

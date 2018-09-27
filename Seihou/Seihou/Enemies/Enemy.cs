using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Seihou
{
    abstract class Enemy : Entity 
    {
        protected int explosionParticles = 5;

        protected Enemy(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
            ec = EntityManager.EntityClass.enemy;
        }

        public override void OnDamaged(Entity by, int damage)
        {
            hp--;
            for (int i = 0; i < explosionParticles; i++)
            {
                em.AddEntity(new Particle(pos, sb, em));
            }
			em.AddEntity(new Power(pos, sb, em));
            em.RemoveEntity(this);
        }

        public override void Update(GameTime gt)
		{
			if (pos.Y > Global.screenHeight + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}
		}

		public override void Draw(GameTime gt)
		{
			base.Draw(gt);
			MonoGame.Primitives2D.DrawCircle(sb, pos, 10, 3, Color.Green, 3);
		}
	}
}

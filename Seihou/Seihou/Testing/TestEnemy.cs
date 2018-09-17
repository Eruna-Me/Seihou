using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    class TestEnemy : Enemy
    {
        public TestEnemy(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, 30, sb, em)
        {
        }

        public override void Damage(Entity by, int damage)
        {
            for (int i = 0; i < 4; i++)
                em.AddEntity(new Particle(pos, sb, em));
            em.RemoveEntity(this);
        }

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.DrawCircle(sb,pos, 10, 15, Color.Green, 3);
        }

        public override void Update(GameTime gt)
        {

            if (Global.random.Next(0,100) == 0)
                em.AddEntity(new Bullet(pos, sb, em, this, new Vector2(0, 500)));

            speed.X = (float)Math.Sin(gt.TotalGameTime.TotalSeconds);

            pos.X += Global.random.Next(-10,11);
            pos.Y += Global.random.Next(-10,11);
        }
    }
}

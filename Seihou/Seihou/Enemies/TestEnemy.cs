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
        Random rnd = new Random();

        TestEnemy(float x, float y, SpriteBatch sb, EntityManager em) : base(x, y, sb, em)
        {

        }

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.DrawCircle(sb, x, y, 10, 15, Color.Green, 3);
        }

        public override void Update(GameTime gt)
        {
            x += rnd.Next(-1,2) * 5;
            y += rnd.Next(-1,2) * 5;
        }
    }
}

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

        TestEnemy(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {

        }

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.DrawCircle(sb,pos, 10, 15, Color.Green, 3);
        }

        public override void Update(GameTime gt)
        {
            pos.X += rnd.Next(-1,2) * 5;
            pos.Y += rnd.Next(-1,2) * 5;
        }
    }
}

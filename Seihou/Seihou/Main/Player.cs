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
    class Player : Entity
    {
        private float speed = 50.3151f;

        public Player(int x,int y,SpriteBatch sb) : base(x, y, sb)
        {
            //Init player blabalbala
        }

        public override void Update(GameTime gt)
        {
            Console.WriteLine("Update");

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                x += speed * (float)gt.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.DrawCircle(sb, x, y, 50, 100, Color.Red, 5);
        }
    }
}

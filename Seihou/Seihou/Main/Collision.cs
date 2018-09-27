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
    static class Collision
    {
        public static bool Circle(Entity a,Entity b)
        {
            float deltaX = (b.pos - a.pos).X;
            float deltaY = (b.pos - a.pos).Y;

            Debugging.Check();

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY) <= a.size + b.size;
        }

        public static bool Circle(Entity a, Vector2 pos, int size)
        {
            float deltaX = (pos - a.pos).X;
            float deltaY = (pos - a.pos).Y;

            Debugging.Check();

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY) <= a.size + size;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

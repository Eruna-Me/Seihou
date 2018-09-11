using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
    class Entity
    {
        static int newId = 0;
        readonly int id;
        int x, y;

        Entity(int x, int y)
        {
            this.x = x;
            this.y = y;

            newId++;
            id = newId;
        }
    }
}

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
        protected Enemy(Vector2 pos,int size, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
            
        }
    }
}

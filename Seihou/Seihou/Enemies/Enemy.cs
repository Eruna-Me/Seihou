﻿using System;
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
        float hp;
        protected Enemy(float x, float y, SpriteBatch sb, EntityManager em) : base(x, y, sb, em)
        {
            
        }
    }
}

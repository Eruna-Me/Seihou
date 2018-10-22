using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Seihou
{
    
    class Boss : Enemy
    {
        readonly Queue<Pattern> patterns = new Queue<Pattern>();

        public Boss(Vector2 pos,SpriteBatch sb,EntityManager em) : base(pos,sb,em)
        {
            
        }

        public override void Update(GameTime gt)
        {
            if (patterns.Count > 0)
            {
                patterns.Peek().Update(gt);
                if (patterns.Peek().finsihed) patterns.Dequeue();
            }

            base.Update(gt);
        }
    }
}

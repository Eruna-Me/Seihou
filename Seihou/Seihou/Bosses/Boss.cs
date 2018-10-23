using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Seihou
{
    
    abstract class Boss : Enemy
    {
        protected enum Stages
        {
            high,
            mid,
            low,
        }

        protected readonly Dictionary<Stages,List<Pattern>> patterns = new Dictionary<Stages, List<Pattern>>();

        //Initialize these
        protected int highHp = 0;
        protected int midHp = 0;
        protected int lowHp = 0;
        

        public Boss(Vector2 pos,SpriteBatch sb,EntityManager em) : base(pos,sb,em)
        {
            patterns[Stages.high] = new List<Pattern>();
            patterns[Stages.mid]  = new List<Pattern>();
            patterns[Stages.low]  = new List<Pattern>(); 
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            Stages pattern = Stages.high;
            if (hp <= midHp)
            {
                pattern = Stages.mid;

                if (pattern != Stages.mid)
                    Debugging.Write(this, "Started MID stage");
            }
            else if (hp <= lowHp)
            {
                pattern = Stages.low;

                if (pattern != Stages.low)
                    Debugging.Write(this, "Started LOW stage");
            }

            bool ok = false;

            foreach (var p in patterns[pattern])
            {
                if (!p.finsihed)
                {
                    ok = true;
                    p.Update(gt);
                    break;
                }
            }

            if (!ok)
            {
                foreach (var p in patterns[pattern]) p.Reset();
                Debugging.Write(this, "Pattern reset");
            }    
        }
    }
}

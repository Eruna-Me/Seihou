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
        protected Stages currentStage = Stages.high;

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
            bool ok = false;

            currentStage = (hp <= midHp) ? ((hp <= lowHp) ? Stages.low : Stages.mid) : Stages.high;
            
            foreach (var p in patterns[currentStage])
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
                foreach (var p in patterns[currentStage]) p.Reset();
                Debugging.Write(this, "Pattern reset");
            }    
        }
    }
}

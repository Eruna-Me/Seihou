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

		//Variables
		protected float timeUntilSkipStage = 60.0f;
		private float skipTimer = 0;
		protected bool wantsToLeave = false;

        public Boss(Vector2 pos,SpriteBatch sb,EntityManager em) : base(pos,sb,em)
        {
            patterns[Stages.high] = new List<Pattern>();
            patterns[Stages.mid]  = new List<Pattern>();
            patterns[Stages.low]  = new List<Pattern>(); 
        }

        public override void Update(GameTime gt)
        {
			skipTimer += (float)gt.ElapsedGameTime.TotalSeconds;

			if (skipTimer > timeUntilSkipStage)
			{
				skipTimer = 0;
				if (currentStage == Stages.low)
					wantsToLeave = true;
				else
					SkipToNextStage();
			}

            base.Update(gt);



            bool ok = false;

			var lastStage = currentStage;
            currentStage = (hp <= midHp) ? ((hp <= lowHp) ? Stages.low : Stages.mid) : Stages.high;

			if (lastStage != currentStage)
			{
				Debugging.Write(this, $"{lastStage} -> {currentStage}");
			}
            
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
                foreach (var p in patterns[currentStage]) p.Reset();
        }

		public void SkipToNextStage()
		{
			hp = (currentStage == Stages.high) ? midHp : lowHp;
			currentStage = (currentStage == Stages.high) ? currentStage = Stages.mid : currentStage = Stages.low;	
		}

		public override void Draw(GameTime gt)
		{
			//Draw boss healthbar
			MonoGame.Primitives2D.FillRectangle(sb, new Vector2(20, 10), new Vector2((float)(Global.playingFieldWidth - 20 * 2) / ((float)highHp / (float)hp), 10), Color.Red, 0);

			base.Draw(gt);
		}

	}
}

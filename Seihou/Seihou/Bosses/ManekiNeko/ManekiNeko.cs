using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    class ManekiNeko : Boss
    {
        private const float borderWidth = 100;
        private const float hoverHeight = 100;
        private const float fallspeed = 20;
        private const float moveSpeed = 100;

        //Variable
        private bool startMoving = false;

        public ManekiNeko(SpriteBatch sb, EntityManager em) : base(new Vector2(Global.Center.X,Global.spawnHeight), sb, em)
        {
            speed.Y = fallspeed;
            texture = "ManekiNeko";

            hp = 40;

            highHp = hp;
            midHp = (int)(hp * 0.5f);
            lowHp = (int)(hp * 0.25f);


            patterns[Stages.low].Add(new CoinThrow(this, em)); //Pattern 1
            patterns[Stages.low].Add(new CoinThrow(this, em)); //Pattern 2
            patterns[Stages.low].Add(new CoinThrow(this, em)); //Pattern 3
            patterns[Stages.low].Add(new CoinThrow(this, em)); //Pattern 4
            patterns[Stages.low].Add(new CoinThrow(this, em)); //Pattern 5
            patterns[Stages.low].Add(new CoinThrow(this, em)); //Pattern 6
            patterns[Stages.low].Add(new CoinThrow(this, em)); //Pattern 7
            patterns[Stages.low].Add(new CoinThrow(this, em)); //Pattern 8
        }

        public override void Update(GameTime gt)
        {
            if (pos.Y > hoverHeight && !startMoving)
            {
                speed.Y = 0;
                speed.X = moveSpeed;
                startMoving = true;
            }

            if (pos.X > Global.playingFieldWidth - borderWidth) speed.X = -moveSpeed;
            if (pos.X < borderWidth) speed.X = moveSpeed;

            pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

            base.Update(gt);
        }
    }
}

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
        private float moveSpeed = 100;
        private const float lowHpSpeed = 300;

        //Variable
        private bool startMoving = false;

        public ManekiNeko(SpriteBatch sb, EntityManager em) : base(new Vector2(Global.Center.X,Global.spawnHeight), sb, em)
        {
            speed.Y = fallspeed;
            texture = "ManekiNeko";

            size = ResourceManager.textures[texture].Height / 2;
            hp = 200;

            highHp = hp;
            midHp = (int)(hp * 0.5f);
            lowHp = (int)(hp * 0.25f);


            patterns[Stages.high].Add(new CoinCircle(this, em, 1f)); 

            patterns[Stages.mid].Add(new CoinThrow(this, em, 1.3f)); 
            patterns[Stages.mid].Add(new CoinDirectional(this, em, 1.3f,5)); 

            patterns[Stages.low].Add(new CoinThrow(this, em, 0.5f));
            patterns[Stages.low].Add(new CoinDirectional(this, em, 0.1f, 1));
            patterns[Stages.low].Add(new CoinCircle(this, em, 1f)); 
        }



        public override void Update(GameTime gt)
        {
            if (currentStage == Stages.low)
            {
                moveSpeed = lowHpSpeed;
            }

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

        public override void OnDamaged(Entity by, int damage)
        {
            hp--;

            if (hp <= 0)
            {
                
                for (int i = 0; i < explosionParticles; i++)
                {
                    em.AddEntity(new Particle(pos, sb, em));
                }

                for (int i = 0; i < 100; i++)
                {
                    Vector2 randomVec = new Vector2(Global.random.Next(-20, 21), Global.random.Next(-20, 21));

                    em.AddEntity(new Power(pos + randomVec, sb, em));

                    randomVec = new Vector2(Global.random.Next(-20, 21), Global.random.Next(-20, 21));

                    em.AddEntity(new Point(pos + randomVec, sb, em));
                }

                em.RemoveEntity(this);
            }
        }
    }
}

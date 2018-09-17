using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
    class Particle : Entity
    {
        
        private readonly float maxLife = 1.0f;
        private float alpha = 1.0f;
        private float life;

        public Particle(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
            life = maxLife;
            size = 3;
            faction = Global.Faction.friendly;
            speed = new Vector2(Global.random.Next(-10,11), Global.random.Next(-10, 11));

        }

        public override void Update(GameTime gt)
        {
            life -= (float)gt.ElapsedGameTime.TotalSeconds;
            alpha = life / maxLife;
            pos += speed;

            if (life <= 0)
                em.RemoveEntity(this);
        }

        public override void Draw(GameTime gt)
        {
            MonoGame.Primitives2D.DrawCircle(sb, pos, size, 100,new Color(Color.Orange,alpha), 2);
        }
    }
}

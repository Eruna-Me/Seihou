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

        private float gravity = 30.0f;
        private readonly float maxLife = 100.0f;
        private float maxSpeed = 10.0f;
        private float alpha = 1.0f;
        private float life;

        public Particle(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
            life = (float)Global.random.NextDouble() * maxLife;
            collision = false;
            size = 3;
            faction = Global.Faction.friendly;
            speed = new Vector2((float)(Global.random.NextDouble() - 0.5) * maxSpeed, (float)(Global.random.NextDouble() - 0.5) * maxSpeed);

        }

        public override void Update(GameTime gt)
        {
            speed.Y += gravity * (float)gt.ElapsedGameTime.TotalSeconds;

            life -= 1;
            alpha = life / maxLife;
            pos += speed;

            if (life <= 0)
                em.RemoveEntity(this);
        }

        public override void Draw(GameTime gt)
        {
            //MonoGame.Primitives2D.DrawCircle(sb, pos, size, 100,new Color(0,0,255,0.2f), 2);
            sb.Draw(SpriteManager.textures["FireParticle"], pos, new Color(new Vector4(alpha, alpha, alpha, alpha)));
        }
    }
}

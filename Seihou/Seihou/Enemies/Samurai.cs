using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//standard faller
namespace Seihou
{
    class Samurai : Enemy
    {
        private const float fallSpeed = 100.0f;

        public Samurai(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {
			texture = "Samurai";
            ec = EntityManager.EntityClass.enemy;
            size = 24; 
            speed.Y = fallSpeed;
			hp = 3;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}

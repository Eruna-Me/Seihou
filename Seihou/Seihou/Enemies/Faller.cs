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
    class Faller : Entity
    {
        public Faller(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
        {

        }


        public override void Update(GameTime gt)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gt)
        {
            throw new NotImplementedException();
        }
    }
}

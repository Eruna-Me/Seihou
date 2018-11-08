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
    class Shuriken : EnemyProjectile
    {
        Trail trail;
        float rotation = 0;
		float acceleration = 1.01f;

        public Shuriken(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em, owner)
        {
            texture = "Shuriken";
            trail = new Trail(sb, texture,20,0.01f);
            this.speed = speed;
            size = ResourceManager.textures[texture].Height/2;
        }

        public override void Update(GameTime gt)
        {
            rotation += (float)gt.ElapsedGameTime.TotalSeconds;
            trail.Update(pos,gt, rotation);
			speed *= acceleration;

            base.Update(gt);
        }

        public override void Draw(GameTime gt)
        {
            trail.Draw(gt);
            sb.Draw(ResourceManager.textures[texture], pos, null, Color.White,(rotation * (float)Math.PI),ResourceManager.Center(texture), 1.0f, SpriteEffects.None, 0f);
        }

    }
}

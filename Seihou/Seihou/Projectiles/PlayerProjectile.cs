﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    abstract class PlayerProjectile : Entity
    {
        protected readonly Entity owner;
        protected int outOfScreenMargin;

        protected PlayerProjectile(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner) : base(pos, sb, em)
        {
            this.owner = owner;
            outOfScreenMargin = -Global.outOfScreenMargin;
        }

        public override void Update(GameTime gt)
        {
            pos += speed * gt.Time();

            Entity c = em.Touching(this, EntityManager.EntityClass.enemy);

            if (c != null)
            {
                if (c.hp > 0 && hp > 0)
                {
                    hp--;
                    c.OnDamaged(owner, 1);
                    em.RemoveEntity(this);
                }
            }

            if (pos.Y + outOfScreenMargin < 0 || pos.Y > Global.screenHeight + outOfScreenMargin || pos.X + outOfScreenMargin < 0 || pos.X > Global.playingFieldWidth + outOfScreenMargin)
            {
                em.RemoveEntity(this);
            }
        }

        public override void Draw(GameTime gt)
        {
            ResourceManager.DrawAngledTexture(sb, texture, pos, speed);
#if DRAWCOLBOX
			MonoGame.Primitives2D.DrawCircle(sb, pos, size, 10, Color.White, 1);
#endif
        }
    }
}

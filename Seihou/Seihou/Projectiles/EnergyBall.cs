﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    class EnergyBall : EnemyProjectile
    {
        Trail trail;
        float rotation = 0;
        public EnergyBall(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed) : base(pos, sb, em, owner)
        {
            texture = "EnergyBall";
            trail = new Trail(sb, texture,20,0.01f);
            this.speed = speed;
            size = ResourceManager.textures[texture].Height/2;
        }

        public override void Update(GameTime gt)
        {
            rotation += gt.Time();
            trail.Update(pos,gt, rotation);

            base.Update(gt);
        }

        public override void Draw(GameTime gt)
        {
            trail.Draw(gt);
            sb.Draw(ResourceManager.textures[texture], pos, null, Color.White,(rotation * (float)Math.PI),ResourceManager.Center(texture), 1.0f, SpriteEffects.None, 0f);   
        }

    }
}

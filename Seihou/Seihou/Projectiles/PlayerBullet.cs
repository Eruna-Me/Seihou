using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    class PlayerBullet : PlayerProjectile
    {
        public PlayerBullet(Vector2 pos, SpriteBatch sb, EntityManager em, Entity owner, Vector2 speed, string texture) : base(pos, sb, em, owner)
        {
            this.texture = texture;
            ec = EntityManager.EntityClass.nonSolid;

            this.speed = speed;
            outOfScreenMargin = ResourceManager.textures[texture].Height;
        }
    }
}

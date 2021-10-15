using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Seihou
{
	class Cloud : Entity
	{
		public const int SPAWN_MARGIN = 1000;

		private readonly Color _color = Color.White;
		private readonly string _texture;
		private readonly float _alpha;
		private readonly float _speed;
		private readonly SpriteEffects _mirror;

		public Cloud(Vector2 pos, SpriteBatch sb, EntityManager em, string texture, float alpha, float speed, bool mirror) : base(pos, sb, em)
		{
			ec = EntityManager.EntityClass.cloud;
			_texture = texture;
			_alpha = alpha;
			_speed = speed;
			_mirror = mirror ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
		}

		public override void Update(GameTime gt)
		{
			pos.Y += _speed * gt.Time();

			if (pos.Y > Global.screenHeight + SPAWN_MARGIN)
			{
				em.RemoveEntity(this);
			}
		}

		public override void Draw(GameTime gt)
		{
			sb.Draw(ResourceManager.textures[_texture], pos, null, _color * _alpha, default, ResourceManager.Center(_texture), 1, _mirror, 0);
		}
	}
}

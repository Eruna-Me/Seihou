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
	abstract class Powerup : Entity
	{
		private const float maxSpeed = 300.0f;
		private const float acceleration = 75.0f;
		protected string texture;

		public Powerup(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
		{
			size = 25;
		}

		public override void Update(GameTime gt)
		{
			speed.Y += acceleration * (float)gt.ElapsedGameTime.TotalSeconds;

			if (speed.Y > maxSpeed) speed.Y = maxSpeed;

			pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

			if (pos.Y + Global.outOfScreenMargin < 0 || pos.Y > Global.screenHeight + Global.outOfScreenMargin || pos.X + Global.outOfScreenMargin < 0 || pos.X > Global.screenWidth + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}
		}

		public override void Draw(GameTime gt)
		{
			sb.Draw(ResourceManager.textures[texture], pos - ResourceManager.Origin(texture), Color.White);
		}
	}
}

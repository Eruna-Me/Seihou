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
		private const float acceleration = 200.0f;
		private const int deltaXspeed = 150;
		private const float xDeceleration1 = 50;
		private const float xDeceleration2 = 200;
		private const float xDeceleration2Margin = 75; //this has to be the worst variable name in the entire project
		private const int homingRange = 64;
		private const float homingSlowness = 15;
		private const float homingSpeed = 400.0f;

		public Powerup(Vector2 pos, SpriteBatch sb, EntityManager em) : base(pos, sb, em)
		{
			size = 12;
			speed.Y = -50;
			speed.X = Global.random.Next(-deltaXspeed, deltaXspeed);
		}

		public override void Update(GameTime gt)
		{
			Entity h = em.Touching(pos, homingRange, EntityManager.EntityClass.player);
			if (h != null || (Global.player.pos.Y < Global.screenHeight * 0.3f))
			{
				speed = (Global.Normalize(Global.player.pos - pos) * homingSpeed + speed * homingSlowness) / (homingSlowness + 1);
			}
			else
			{ 
				speed.Y += acceleration * (float)gt.ElapsedGameTime.TotalSeconds;

				if (speed.X > 0) speed.X -= (speed.X > xDeceleration2Margin ? xDeceleration2 : xDeceleration1) * (float)gt.ElapsedGameTime.TotalSeconds;
				if (speed.X < 0) speed.X += (speed.X < xDeceleration2Margin ? xDeceleration2 : xDeceleration1) * (float)gt.ElapsedGameTime.TotalSeconds;
			}

			if (speed.Y > maxSpeed) speed.Y = maxSpeed;

			pos += speed * (float)gt.ElapsedGameTime.TotalSeconds;

			if (pos.X > Global.playingFieldWidth) pos.X = Global.playingFieldWidth;
			if (pos.X < 0) pos.X = 0;

			if (pos.Y + Global.outOfScreenMargin < 0 || pos.Y > Global.screenHeight + Global.outOfScreenMargin)
			{
				em.RemoveEntity(this);
			}
		}
	}
}

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
	class ScoreGain : Entity
	{
		private float timer = 3.0f;
		private float score = 0;

		public ScoreGain(Vector2 pos, SpriteBatch sb, EntityManager em, float score) : base(pos, sb, em)
		{
			this.score = score;
		}

		public override void Update(GameTime gt)
		{
			timer -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;
			if (timer <= 0) em.RemoveEntity(this); ;
		}

		public override void Draw(GameTime gt)
		{
			sb.DrawString(ResourceManager.fonts["DefaultFont"], score.ToString(), pos, Color.White);
		}
	}
}

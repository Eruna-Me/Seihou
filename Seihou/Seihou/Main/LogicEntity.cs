using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	abstract class LogicEntity : Entity
	{
		protected float? Duration { get; private set; }
		protected float? TimeLeft => _time;
		private float? _time;

		public LogicEntity(EntityManager em, SpriteBatch sb, float? timer = null) : base(default, sb, em)
		{
			ec = EntityManager.EntityClass.logic;
			_time = timer;
			Duration = timer;
		}

		public override void OnDamaged(Entity by, int damage)
		{
		}

		public override void Draw(GameTime gt)
		{
		}

		public override void Update(GameTime gt)
		{
			if (_time.HasValue)
			{
				_time -= gt.Time();
				if (_time < 0)
				{
					OnTimerFinished();
					em.RemoveEntity(this);
				}
			}
		}

		protected virtual void OnTimerFinished()
		{

		}
	}
}

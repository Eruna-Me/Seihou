using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou.Level.Logic
{
	class LogicPause : LogicEntity
	{
		private readonly LevelManager _levelManager;

		public LogicPause(LevelManager levelManager, EntityManager entityManager, [Param("Time")]float time) : base(entityManager, default, time)
		{
			_levelManager = levelManager;
		}

		public override void Update(GameTime gt)
		{
			base.Update(gt);
		}

		protected override void OnTimerFinished()
		{
			_levelManager.Unpause(this);
		}

		public override void OnSpawn()
		{
			_levelManager.Pause(this);
		}
	}
}

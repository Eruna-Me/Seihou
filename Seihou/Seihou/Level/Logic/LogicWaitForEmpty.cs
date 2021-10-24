using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou.Level.Logic
{
	class LogicWaitForEmpty : LogicEntity
	{
		private readonly LevelManager _levelManager;

		public LogicWaitForEmpty(LevelManager levelManager, EntityManager entityManager) : base(entityManager, default, null)
		{
			_levelManager = levelManager;
		}

		public override void Update(GameTime gt)
		{
			if (em.GetEntityCount(EntityManager.EntityClass.enemy) == 0 &&
				em.GetEntityCount(EntityManager.EntityClass.enemyProjectile) == 0)
			{
				_levelManager.Unpause(this);
				em.RemoveEntity(this);
			}
		}

		public override void OnSpawn()
		{
			_levelManager.Pause(this);
		}
	}
}

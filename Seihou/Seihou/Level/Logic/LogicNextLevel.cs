using Microsoft.Xna.Framework.Graphics;

namespace Seihou.Level.Logic
{
    internal class LogicNextLevel : LogicEntity
    {
        private readonly LevelManager _levelManager;
        private readonly string _nextLevel;

        public LogicNextLevel(LevelManager levelManager, EntityManager em, SpriteBatch sb, [Param("NextLevelName")] string nextLevelName) : base(em, sb, null)
        {
            _levelManager = levelManager;
            _nextLevel = nextLevelName;
        }

        public override void OnSpawn()
        {
            _levelManager.LoadLevel(_nextLevel);
        }
    }
}

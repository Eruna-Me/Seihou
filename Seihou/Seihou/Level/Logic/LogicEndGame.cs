using Microsoft.Xna.Framework.Graphics;

namespace Seihou.Level.Logic
{
    internal class LogicEndGame : LogicEntity
    {
        public LogicEndGame(EntityManager em, SpriteBatch sb) : base(em, sb, null) { }

        public override void OnSpawn()
        {
            ((MainState)Global.player.sm.GetCurrentState()).OnGameOver();
        }
    }
}

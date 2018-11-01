using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Seihou
{
	class SettingsState : State
	{
		public SettingsState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm, double score, string mode) : base(sm, cm, sb, gdm)
		{

		}

		public override void Draw(GameTime gt)
		{
			throw new NotImplementedException();
		}

		public override void Update(GameTime gt)
		{
			throw new NotImplementedException();
		}
	}
}

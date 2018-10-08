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
	class LenovoDenovoMan : Player
	{
		public LenovoDenovoMan(SpriteBatch sb, EntityManager em, StateManager sm, State state) : base(sb, em, sm, state)
		{
			texture = "Lenovo-DenovoMan";
		} 
	}
}

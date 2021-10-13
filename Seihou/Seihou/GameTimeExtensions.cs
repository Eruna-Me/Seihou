using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	public static class GameTimeExtensions
	{
		public static float Time(this GameTime gt)
		{
			return (float)gt.ElapsedGameTime.TotalSeconds;
		}
	}
}

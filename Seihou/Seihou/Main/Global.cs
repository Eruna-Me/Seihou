using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	static class Global
	{
		//	SCREENSIZE
		public static readonly int screenWidth = 800;
		public static readonly int screenHeight = 600;
		public static readonly int outOfScreenMargin = 100;

        public enum Faction
        {
            friendly,
            enemy,
            neutral,
            noFaction
        }
	}
}

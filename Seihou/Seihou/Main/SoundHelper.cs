using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	public static class SoundHelper
	{
		public static void PlayRandom(string name)
		{
			int max;
			for (int i = 1; ; i++)
			{
				if (!ResourceManager.soundEffects.TryGetValue($"{name}{i}", out _))
				{
					max = i;
					break;
				}
			}

			ResourceManager.soundEffects[$"{name}{Global.random.Next(1, max)}"].Play();
		}
	}
}

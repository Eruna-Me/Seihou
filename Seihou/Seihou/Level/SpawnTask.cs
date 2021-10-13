using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	record SpawnTask
	{
		public string EntityName { get; init; }

		public Vector2 WorldPosition { get; init; }
		public Vector2 Size { get; init; }
		public Vector2 CenterWorld => WorldPosition + Size / 2;
	}
}

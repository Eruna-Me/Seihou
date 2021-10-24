using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	record EntityCreationData
	{
		public string EntityName { get; init; }
		public Vector2 Position { get; init; }
		public IReadOnlyDictionary<string, string> Properties { get; init; }
	}
}

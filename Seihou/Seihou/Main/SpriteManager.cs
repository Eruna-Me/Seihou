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
	static class SpriteManager
	{
		public static readonly Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
		public static ContentManager cm;


		public static void Load(ContentManager com)
		{
			cm = com;

			textures.Add("FireParticle", cm.Load<Texture2D>("FireParticle"));
			textures.Add("Dart1", cm.Load<Texture2D>("Dart1"));
		}

		public static Vector2 Origin(string texture) => new Vector2(textures[texture].Width / 2, textures[texture].Height / 2);

		public static void DrawAngledTexture(SpriteBatch sb, string texture, Vector2 pos, Vector2 speed)
		{
			sb.Draw(textures["Dart1"], pos, null, Color.White, Global.VtoD(speed), Origin("Dart1"), 1.0f, SpriteEffects.None, 0f);
		}
	}
}

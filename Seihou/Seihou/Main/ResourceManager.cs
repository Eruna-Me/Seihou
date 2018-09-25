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
	static class ResourceManager
	{
		public static readonly Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
		public static readonly Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
		public static ContentManager cm;


		public static void Load(ContentManager com)
		{
			cm = com;

			fonts.Add("DefaultFont", cm.Load<SpriteFont>("KosugiMaru-Medium"));
			textures.Add("FireParticle", cm.Load<Texture2D>("FireParticle"));
			textures.Add("Dart1", cm.Load<Texture2D>("Dart1"));
			textures.Add("Lenovo-DenovoMan", cm.Load<Texture2D>("Lenovo-DenovoMan"));
            textures.Add("Logo", cm.Load<Texture2D>("Logo"));
            textures.Add("EnemyBullet", cm.Load<Texture2D>("EnemyBullet"));
			textures.Add("Point", cm.Load<Texture2D>("Point"));
			textures.Add("Power", cm.Load<Texture2D>("Power"));
		}

		public static Vector2 Origin(string texture) => new Vector2(textures[texture].Width / 2, textures[texture].Height / 2);

		public static void DrawAngledTexture(SpriteBatch sb, string texture, Vector2 pos, Vector2 speed)
		{
			sb.Draw(textures[texture], pos, null, Color.White, Global.VtoD(speed), Origin(texture), 1.0f, SpriteEffects.None, 0f);
		}
	}
}

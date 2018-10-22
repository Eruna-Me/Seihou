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

            //Spritefonts
            fonts.Add("DefaultFont",         cm.Load<SpriteFont>("Spritefonts/KosugiMaru-Medium"));
            fonts.Add("DefaultFontBig",      cm.Load<SpriteFont>("Spritefonts/KosugiMaru-Big"));
                                
            //Projectiles
            textures.Add("Dart1",            cm.Load<Texture2D>("Projectiles/Dart1"));
            textures.Add("Dart2",            cm.Load<Texture2D>("Projectiles/Dart2"));
            textures.Add("EnemyBullet",      cm.Load<Texture2D>("Projectiles/EnemyBullet"));
			textures.Add("Snowflake",		 cm.Load<Texture2D>("Projectiles/Snowflake"));
			textures.Add("Laser1",           cm.Load<Texture2D>("Projectiles/Laser1"));
            textures.Add("Laser2",           cm.Load<Texture2D>("Projectiles/Laser2"));
            textures.Add("Laser3",           cm.Load<Texture2D>("Projectiles/Laser3"));
			textures.Add("EnergyBall", cm.Load<Texture2D>("Projectiles/EnergyBall"));

			//Bosses
			textures.Add("ManekiNeko",		 cm.Load<Texture2D>("Bosses/ManekiNeko"));

            //Enemies
            textures.Add("YukiOnna",		 cm.Load<Texture2D>("Enemies/YukiOnna"));
			textures.Add("Samurai",			 cm.Load<Texture2D>("Enemies/Samurai"));
			textures.Add("MeanMan",          cm.Load<Texture2D>("Enemies/Meanman"));
            textures.Add("KawaiiMan",        cm.Load<Texture2D>("Enemies/KawaiiMan"));
			textures.Add("JSF",				 cm.Load<Texture2D>("Enemies/JSF"));

			//Powerups
			textures.Add("Point",            cm.Load<Texture2D>("Powerups/Point"));
            textures.Add("Power",            cm.Load<Texture2D>("Powerups/Power"));
			textures.Add("LiveUp",			 cm.Load<Texture2D>("Powerups/LiveUp"));

			//Players
			textures.Add("Lenovo-DenovoMan", cm.Load<Texture2D>("Player/Lenovo-DenovoMan"));

            //Misc
            textures.Add("Logo",             cm.Load<Texture2D>("Misc/Logo"));
            textures.Add("SeihouText1",      cm.Load<Texture2D>("Misc/SeihouText1"));
            textures.Add("Tree",             cm.Load<Texture2D>("Misc/Tree"));
			textures.Add("backgroundUI",	 cm.Load<Texture2D>("Misc/backgroundUI"));

			//Other
			textures.Add("FireParticle",     cm.Load<Texture2D>("FireParticle"));

            //Wallpapers
            textures.Add("Wallpaper1",       cm.Load<Texture2D>("Wallpapers/Wallpaper1"));

			//Clouds
			textures.Add("Cloud1",			 cm.Load<Texture2D>("Clouds/Cloud1"));

        }

		public static Vector2 Center(string texture) => new Vector2(textures[texture].Width / 2, textures[texture].Height / 2);

		public static void DrawAngledTexture(SpriteBatch sb, string texture, Vector2 pos, Vector2 speed)
		{
			sb.Draw(textures[texture], pos, null, Color.White, Global.VtoD(speed), Center(texture), 1.0f, SpriteEffects.None, 0f);
		}
	}
}

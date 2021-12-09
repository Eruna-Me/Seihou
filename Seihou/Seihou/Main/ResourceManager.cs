using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Seihou
{
	static class ResourceManager
	{
		public static readonly Dictionary<string, SoundEffect> soundEffects = new();
		public static readonly Dictionary<string, Song> songs = new();

		public static readonly Dictionary<string, Texture2D> textures = new();
		public static readonly Dictionary<string, SpriteFont> fonts = new();
		public static ContentManager cm;

		public static void Load(ContentManager contentManager)
		{
			cm = contentManager;

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
			textures.Add("EnergyBall",       cm.Load<Texture2D>("Projectiles/EnergyBall"));
            textures.Add("Coin",             cm.Load<Texture2D>("Projectiles/Coin"));
			textures.Add("Tanto",			 cm.Load<Texture2D>("Projectiles/Tanto"));
			textures.Add("Shuriken",         cm.Load<Texture2D>("Projectiles/Shuriken"));

			//Bosses
			textures.Add("ManekiNeko",		 cm.Load<Texture2D>("Bosses/ManekiNeko"));

			//Enemies
			textures.Add("Ninja",	         cm.Load<Texture2D>("Enemies/Ninja"));
			textures.Add("Mine",             cm.Load<Texture2D>("Enemies/Mine"));
            textures.Add("YukiOnna",		 cm.Load<Texture2D>("Enemies/YukiOnna"));
			textures.Add("Samurai",			 cm.Load<Texture2D>("Enemies/Samurai"));
			textures.Add("MeanMan",          cm.Load<Texture2D>("Enemies/Meanman"));
            textures.Add("KawaiiMan",        cm.Load<Texture2D>("Enemies/KawaiiMan"));
			textures.Add("JSF",				 cm.Load<Texture2D>("Enemies/JSF"));
			textures.Add("Kitsune",          cm.Load<Texture2D>("Enemies/Kitsune"));

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
            textures.Add("DialogBox",        cm.Load<Texture2D>("Misc/DialogBox"));
            textures.Add("PfpGame",          cm.Load<Texture2D>("Misc/PfpGame"));

			//UI
			textures.Add("Heart",			 cm.Load<Texture2D>("Misc/Hartje"));
			textures.Add("Bomb",			 cm.Load<Texture2D>("Misc/Bomb"));
			textures.Add("backgroundUI",	 cm.Load<Texture2D>("Misc/backgroundUI"));

			//Other
			textures.Add("FireParticle",     cm.Load<Texture2D>("FireParticle"));

            //Wallpapers
            textures.Add("Wallpaper1",       cm.Load<Texture2D>("Wallpapers/Wallpaper1"));

			//Clouds
			textures.Add("Cloud1",			 cm.Load<Texture2D>("Clouds/Cloud1"));
			textures.Add("Cloud2",			 cm.Load<Texture2D>("Clouds/Cloud2"));
			textures.Add("Cloud3",			 cm.Load<Texture2D>("Clouds/Cloud3"));
			textures.Add("Cloud4",			 cm.Load<Texture2D>("Clouds/Cloud4"));

			//Audio
			songs.Add("TestSong", cm.Load<Song>("Audio/Songs/Testsong"));

			soundEffects.Add("EnemyPain1", cm.Load<SoundEffect>("Audio/Effects/EnemyPain1"));
			soundEffects.Add("Shoot1", cm.Load<SoundEffect>("Audio/Effects/Shoot1"));
			soundEffects.Add("Shoot2", cm.Load<SoundEffect>("Audio/Effects/Shoot2"));
			soundEffects.Add("Shoot3", cm.Load<SoundEffect>("Audio/Effects/Shoot3"));

			soundEffects.Add("ExplosionShort1", cm.Load<SoundEffect>("Audio/Effects/ExplosionShort1"));
			soundEffects.Add("ExplosionLong1", cm.Load<SoundEffect>("Audio/Effects/ExplosionLong1"));
		}

		public static Vector2 Center(string texture) => new Vector2(textures[texture].Width / 2, textures[texture].Height / 2);

		public static void DrawAngledTexture(SpriteBatch sb, string texture, Vector2 pos, Vector2 speed)
		{
			sb.Draw(textures[texture], pos, null, Color.White, Global.VtoD(speed), Center(texture), 1.0f, SpriteEffects.None, 0f);
		}
	}
}

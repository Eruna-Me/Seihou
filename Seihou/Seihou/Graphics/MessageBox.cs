using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Seihou
{
	class MessageBox : Entity
    {
        public Color color = Color.White;

        private readonly float maxLife;
        private readonly float maxFadeIn;
        private readonly float maxFadeOut;

        private float life;
        private float fadeIn;
        private float fadeOut;

        private readonly string text;
        private readonly string fontName;

        private float alpha = 0f;
        private readonly float maxAlpha;
		public static bool waitForButtonPress = false;
		public bool waitForButtonPressOn = false;

        public MessageBox(Vector2 pos, SpriteBatch sb,EntityManager em,string text, float life = 2.0f,float fadeIn = 2.0f,float fadeOut = 2.0f,string fontName = "DefaultFontBig",float maxAlpha = 1.0f) : base(pos, sb, em)
        {
            ec = EntityManager.EntityClass.ui;

            this.fontName = fontName;
            this.text = text;

            maxFadeIn = fadeIn;
            this.fadeIn = maxFadeIn;

            maxFadeOut = fadeOut;
            this.fadeOut = maxFadeOut;

            this.maxAlpha = maxAlpha;

            maxLife = life;
            life = maxLife;
        }

        public override void Draw(GameTime gt)
        {
            sb.DrawString(ResourceManager.fonts[fontName], text, pos, new Color(color, alpha), 0, ResourceManager.fonts[fontName].MeasureString(text) / 2, 1, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gt)
        {
			if (waitForButtonPressOn) waitForButtonPress = true; waitForButtonPressOn = false;
			if (fadeIn < 0)
			{
				if (waitForButtonPress)
				{
					if (Keyboard.GetState().IsKeyDown(Settings.GetKey("shootKey")))
					{
						waitForButtonPress = false;
					}
				}
				else
				{
					if (life < 0)
					{
						if (fadeOut > 0)
						{
							fadeOut -= gt.Time();
							alpha = fadeOut / maxFadeOut - (1 - maxAlpha);
						}
						else
						{
							em.RemoveEntity(this);
						}
					}
					else
					{
						life -= gt.Time();
					}
				}
				
			}
			else
			{
				fadeIn -= gt.Time();
				alpha = 1 - fadeIn / maxFadeIn - (1 - maxAlpha);
			}
        }
    }
}

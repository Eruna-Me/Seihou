using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    class MessageBox : Entity
    {
        private float maxLife;
        private float maxFadeIn;
        private float maxFadeOut;

        private float life;
        private float fadeIn;
        private float fadeOut;

        private string text;
        private string fontName;

        private float alpha = 0;

        public MessageBox(Vector2 pos, SpriteBatch sb,EntityManager em,string text, float life = 2.0f,float fadeIn = 2.0f,float fadeOut = 2.0f,string fontName = "DefaultFontBig") : base(pos, sb, em)
        {
            this.fontName = fontName;
            this.text = text;

            maxFadeIn = fadeIn;
            this.fadeIn = maxFadeIn;

            maxFadeOut = fadeOut;
            this.fadeOut = maxFadeOut;

            maxLife = life;
            life = maxLife;
        }

        public override void Draw(GameTime gt)
        {
            sb.DrawString(ResourceManager.fonts[fontName], text, pos,new Color(new Vector4(alpha,alpha,alpha,alpha)), 0, ResourceManager.fonts[fontName].MeasureString(text) / 2, 1, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gt)
        {
            if (fadeIn < 0)
            {
                if (life < 0)
                {
                    if (fadeOut > 0)
                    {
                        fadeOut -= (float)gt.ElapsedGameTime.TotalSeconds;
                        alpha = fadeOut / maxFadeOut;
                    }
                }
                else
                {
                    life -= (float)gt.ElapsedGameTime.TotalSeconds;
                }
            }
            else
            {
                fadeIn -= (float)gt.ElapsedGameTime.TotalSeconds;
                alpha = 1 - fadeIn / maxFadeIn;
            }
        }
    }
}

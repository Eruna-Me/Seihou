using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
    class Trail
    {
        readonly int length;
        private List<Vector2> sections = new List<Vector2>();
        private string texture;
        private SpriteBatch sb;

        public Trail(int length,SpriteBatch sb,string texture)
        {
            this.sb = sb;
            this.texture = texture;
            this.length = length;
        }

        public void AddSection(Vector2 v)
        {
            if (Settings.SimpleGraphics) return;
            sections.Add(v);

            if (sections.Count > 10)
            {
                sections.Remove(sections.First());
            }
        }

        public void Draw(GameTime gt)
        {
            if (Settings.SimpleGraphics) return;
            for (int i = 0; i < sections.Count; i++)
            {
                float alpha = 0.05f;
                sb.Draw(ResourceManager.textures[texture],sections[i] - ResourceManager.Center(texture),new Color(new Vector4(alpha,alpha,alpha,alpha)));
            }
        }
    }
}

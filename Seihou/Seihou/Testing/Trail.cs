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
        private struct Section
        {
            public float rotation;
            public Vector2 pos;
        }

        readonly int length;
        private List<Section> sections = new List<Section>();
        private string texture;
        private SpriteBatch sb;

        public Trail(int length,SpriteBatch sb,string texture)
        {
            this.sb = sb;
            this.texture = texture;
            this.length = length;
        }

        public void AddSection(Vector2 v,float rotation)
        {
            if (Settings.SimpleGraphics) return;
            sections.Add(new Section() { pos = v, rotation = rotation });

            if (sections.Count > length)
            {
                sections.Remove(sections.First());
            }
        }

        public void AddSection(Vector2 v)
        {
            if (Settings.SimpleGraphics) return;
            sections.Add(new Section(){pos = v,rotation = 0});

            if (sections.Count > length)
            {
                sections.Remove(sections.First());
            }
        }

        public void Draw(GameTime gt)
        {
            if (Settings.SimpleGraphics) return;
            for (int i = 0; i < sections.Count; i++)
            {
                float alpha = 0.1f;
                sb.Draw(ResourceManager.textures[texture],sections[i].pos, null,new Color(new Vector4(alpha,alpha,alpha,alpha)),sections[i].rotation,ResourceManager.Center(texture), 1.0f, SpriteEffects.None, 0f);
            }
        }
    }
}

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
    class SpriteManager
    {
        public static readonly Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        void LoadTextures(ContentManager cm)
        {
            //textures.Add("Test", cm.Load<Texture2D>("Texture name"));
        }
    }
}

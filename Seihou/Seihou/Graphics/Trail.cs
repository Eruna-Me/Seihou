using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Seihou
{
	class Trail
    {
        struct Section
        {
            public float rotation;
            public Vector2 pos;
        }

		//Spawning
        readonly int amount;
		readonly float spawnTime;
		float spawnTimer = 0;

        List<Section> sections = new List<Section>();
        readonly string texture;
        SpriteBatch sb;

        public Trail(SpriteBatch sb,string texture,int amount,float spawnTime)
        {
            this.sb = sb;
            this.texture = texture;
            this.amount = amount;
			this.spawnTime = spawnTime;
        }

        public void Update(Vector2 v,GameTime gt,float rotation = 0)
        {
			spawnTimer += gt.Time();

			if (spawnTimer > spawnTime)
			{
				spawnTimer = 0;
				if (Settings.GetBool("simpleGraphics")) return;
				sections.Add(new Section() { pos = v, rotation = rotation });

				if (sections.Count > amount)
				{
					sections.Remove(sections.First());
				}
			}
        }

        public void Draw(GameTime gt)
        {
            if (Settings.GetBool("simpleGraphics")) return;
            for (int i = 0; i < sections.Count; i++)
            {
                float alpha = 0.1f;
                sb.Draw(ResourceManager.textures[texture],sections[i].pos, null,new Color(new Vector4(alpha,alpha,alpha,alpha)),sections[i].rotation,ResourceManager.Center(texture), 1.0f, SpriteEffects.None, 0f);
            }
        }
    }
}

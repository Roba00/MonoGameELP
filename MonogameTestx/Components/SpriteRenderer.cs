using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameELP.Components
{
    class SpriteRenderer
    {
        public Texture2D Texture { get; private set; }

        public SpriteRenderer(Texture2D texture)
        {
            Texture = texture;
        }
    }
}

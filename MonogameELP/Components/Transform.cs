using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonogameELP.Components
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }

        public void Translate(float x, float y)
        {
            Position = new Vector2(Position.X + x, Position.Y + y);
        }
    }
}

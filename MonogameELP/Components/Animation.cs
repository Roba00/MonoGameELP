using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameELP.Components
{
    public class Animation
    {
        private Texture2D texture;
        public int CurrentFrame { get; set; }
        public int FrameStart { get; private set; }
        public int FrameEnd { get; private set; }
        public int TotalFramesInTexture { get; private set; }
        public int FrameHeight { get { return texture.Height; } }
        public float FrameSpeed { get; set; }
        public int FrameWidth { get { return texture.Width / TotalFramesInTexture; } }
        public bool IsLooping { get; set; }

        public Animation(Texture2D texture, int frameStart, int frameEnd, int frameTotalInTexture, float frameSpeed, bool isLooping)
        {
            this.texture = texture;
            FrameStart = frameStart;
            FrameEnd = frameEnd;
            TotalFramesInTexture = frameTotalInTexture;
            FrameSpeed = frameSpeed;
            IsLooping = isLooping;
        }
    }
}

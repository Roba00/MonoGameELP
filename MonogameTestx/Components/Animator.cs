using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameELP.Components
{
    class Animator
    {
        private Animation animation;
        private float timer;

        public Animator(Animation animation)
        {
            this.animation = animation;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > animation.FrameSpeed)
            {
                timer = 0f;
                animation.CurrentFrame++;

                if (animation.CurrentFrame > animation.FrameEnd)
                {
                    animation.CurrentFrame = animation.FrameStart;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
        {
            spriteBatch.Draw(texture: texture,
                             position: position,
                             sourceRectangle: new Rectangle(x: animation.CurrentFrame * animation.FrameWidth,
                                                           y: 0,
                                                           width: animation.FrameWidth,
                                                           height: animation.FrameHeight),
                             color: Color.White,
                             rotation: 0f,
                             origin: new Vector2(animation.FrameWidth/2, animation.FrameHeight/2),
                             scale: 6.25f,
                             effects: SpriteEffects.None,
                             layerDepth: 0.5f);
        }
        
        public void Play(Animation animation)
        {
            if (this.animation == animation)
                return;
            this.animation = animation;
            this.animation.CurrentFrame = this.animation.FrameStart;
            timer = 0f;
        }

        public void Stop()
        {
            timer = 0f;
            animation.CurrentFrame = animation.FrameStart;
        }

    }
}

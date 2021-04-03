using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameELP.Components
{
    public class Animator
    {
        private Animation animation;
        private Animation defaultAnimation;
        private float timer;

        public Animator(Animation defaultAnimation)
        {
            animation = defaultAnimation;
            this.defaultAnimation = defaultAnimation;
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
                    if (!animation.IsLooping)
                    {
                        animation = defaultAnimation;
                    }
                    else
                    {
                        animation.CurrentFrame = animation.FrameStart;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Transform transform)
        {
            spriteBatch.Draw(texture: texture,
                             position: transform.Position,
                             sourceRectangle: new Rectangle(x: animation.CurrentFrame * animation.FrameWidth,
                                                           y: 0,
                                                           width: animation.FrameWidth,
                                                           height: animation.FrameHeight),
                             color: Color.White,
                             rotation: 0f,
                             origin: new Vector2(animation.FrameWidth*0.25f, animation.FrameHeight*0.75f),
                             scale: transform.Scale,
                             effects: SpriteEffects.None,
                             layerDepth: 0.5f);
        }
        
        public void Play(Animation animation)
        {
            if (this.animation == animation || !this.animation.IsLooping)
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

        public Animation GetState()
        {
            return animation;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameELP.Components;

namespace MonogameELP.Gameobjects
{
    class Player
    {
        private Transform transform;
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private Dictionary<string, Animation> animations;


        public Player()
        {
            transform = new Transform();
        }

        public void Initialize()
        {
            transform.Position = new Vector2(250, 250);
            transform.Rotation = 0f;
            transform.Scale = new Vector2(6.25f, 6.25f);
        }

        public void LoadContent()
        {
            spriteRenderer = new SpriteRenderer(Game1.plyrTxtr);
            animations = new Dictionary<string, Animation>()
            {
                { "Idle", new Animation(spriteRenderer.Texture, 0, 1, 9, 1f) },
                { "Walk", new Animation(spriteRenderer.Texture, 2, 4, 9, 0.1f) },
                { "Attack", new Animation(spriteRenderer.Texture, 5, 8, 9, 0.1f) }
            };
            animator = new Animator(animations["Idle"]);
        }

        public void Update (GameTime gameTime)
        {
            animator.Play(animations["Idle"]);
            animator.Update(gameTime);
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            animator.Draw(spriteBatch, spriteRenderer.Texture, transform.Position);
        }
    }
}

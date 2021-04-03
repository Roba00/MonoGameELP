using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameELP.Components;

namespace MonogameELP.Gameobjects
{
    class Ground : GameObject
    {
        public Transform transform;
        private BoxCollider collider;
        private Rigidbody rigidBody;
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private Dictionary<string, Animation> animations;

        public void Initialize()
        {
            transform = new Transform();
        }

        public void LoadContent()
        {
            transform.Position = new Vector2(250, 100f);
            transform.Rotation = 0f;
            transform.Scale = new Vector2(6.25f, 6.25f);
        }

        public void Update(GameTime gameTime)
        {
            collider = new BoxCollider(transform, true);
            rigidBody = new Rigidbody(transform, collider, 1);
            spriteRenderer = new SpriteRenderer(Game1.plyrTxtr);
            animations = new Dictionary<string, Animation>()
            {
                { "Idle", new Animation(spriteRenderer.Texture, 0, 0, 1, 1f, false) },
            };
            animator = new Animator(animations["Idle"]);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animator.Draw(spriteBatch, spriteRenderer.Texture, transform);
        }
    }
}

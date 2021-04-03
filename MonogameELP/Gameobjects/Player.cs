using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameELP.Components;

namespace MonogameELP.Gameobjects
{
    class Player : GameObject
    {
        public Transform transform;
        private BoxCollider collider;
        private Rigidbody rigidBody;
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private Dictionary<string, Animation> animations;

        private bool isJumpingInAir = false;
        private bool isJumpButtonReleased = false;
        private float jumpSpeed = 200f;
        private float jumpTimeCounter = 0.2f;
        private float jumpTimeTotal = 50f;

        public Player()
        {
            transform = new Transform();
        }

        public void Initialize()
        {
            transform.Position = new Vector2(500f, 150f);
            transform.Rotation = 0f;
            transform.Scale = new Vector2(8f, 8f);
        }

        public void LoadContent()
        {
            collider = new BoxCollider(transform, true);
            rigidBody = new Rigidbody(transform, collider, 40, 7.5f);
            spriteRenderer = new SpriteRenderer(Game1.plyrTxtr);
            CreateAnimations();
            animator = new Animator(animations["Idle"]);
        }

        public void Update(GameTime gameTime)
        {
            Input.UpdateState();

            WalkControl();
            AttackControl();
            JumpControl(gameTime);

            rigidBody.Update(gameTime);
            collider.Update();
            animator.Update(gameTime);

            //System.Diagnostics.Debug.WriteLine("Player Position: (" + transform.Position.X + ", " + transform.Position.Y + ").");
            //System.Diagnostics.Debug.WriteLine("Player Velocity: " + rigidBody.Velocity.Y);
            //System.Diagnostics.Debug.WriteLine("Player Acceleration: " + rigidBody.Acceleration.Y);
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            animator.Draw(spriteBatch, spriteRenderer.Texture, transform);
            /*Texture2D test = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            test.SetData(new[] { Color.White });
            spriteBatch.Draw(test, collider.Rectangle, Color.Black);*/
        }

        void CreateAnimations()
        {
            animations = new Dictionary<string, Animation>()
            {
                { "Idle", new Animation(spriteRenderer.Texture, 0, 1, 18, 1f, true) },
                { "Walk", new Animation(spriteRenderer.Texture, 2, 4, 18, 0.1f, true) },
                { "Attack", new Animation(spriteRenderer.Texture, 5, 8, 18, 0.085f, false) },
                { "Jump", new Animation(spriteRenderer.Texture, 9, 9, 18, 1f, false) },
                { "JumpAttackUp", new Animation(spriteRenderer.Texture, 10, 13, 18, 0.125f, false) },
                { "JumpAttackDown", new Animation(spriteRenderer.Texture, 14, 17, 18, 0.125f, false) }
            };
        }

        void WalkControl()
        {
            if (Input.GetLeft() && animator.GetState() != animations["Attack"])
            {
                transform.Scale = new Vector2(-Math.Abs(transform.Scale.X), transform.Scale.Y);
                rigidBody.SetVelocity(-100, rigidBody.Velocity.Y);
                if (animator.GetState() != animations["Jump"])
                    animator.Play(animations["Walk"]);
            }
            else if (Input.GetRight() && animator.GetState() != animations["Attack"])
            {
                transform.Scale = new Vector2(Math.Abs(transform.Scale.X), transform.Scale.Y);
                rigidBody.SetVelocity(100, rigidBody.Velocity.Y);
                if (animator.GetState() != animations["Jump"])
                    animator.Play(animations["Walk"]);
            }
            else
            {
                rigidBody.SetVelocity(0, rigidBody.Velocity.Y);
                if (animator.GetState() != animations["Jump"])
                    animator.Play(animations["Idle"]);
            }
        }

        void AttackControl()
        {
            if (Input.GetZ_Down())
            {
                animator.Play(animations["Attack"]);
            }
        }

        void JumpControl(GameTime gt)
        {
            //If get jump while is grounded, then jump and start timer
            if (Input.GetX_Down() && collider.isGrounded())
            {

                System.Diagnostics.Debug.WriteLine("Got jump button");
                rigidBody.SetGravityScale(40f);
                isJumpingInAir = true;
                isJumpButtonReleased = false;
                jumpTimeCounter = jumpTimeTotal;
                animator.Play(animations["Jump"]);
                rigidBody.SetVelocity(rigidBody.Velocity.X, -jumpSpeed);

                System.Diagnostics.Debug.WriteLine("Counting Timer, " + jumpTimeCounter + " seconds in progress");
                jumpTimeCounter -= (float)gt.ElapsedGameTime.TotalSeconds;
            }
            //If jump button is released or  the timer exceeds the limit, then stop jumping.
            if (Input.GetX_Up() || jumpTimeCounter >= jumpTimeTotal)
            {
                System.Diagnostics.Debug.WriteLine("Stop Jumping: Jump Button Up or Timer Exceeded");
                jumpTimeCounter = 0;
                isJumpButtonReleased = true;
                isJumpingInAir = false;
                //animator.Play(animations["Idle"]);
                //animator.Play(animations["Idle"]);
            }
            //If collides to ground after jumping, then stop jumping
            /*if (isJumpingInAir && collider.isGrounded() && jumpTimeCounter > 0.1f)
            {
                System.Diagnostics.Debug.WriteLine("Stop Jumping: Collided With Ground");
                jumpTimeCounter = 0;
                isJumpButtonReleased = true;
                isJumpingInAir = false;
            }*/
            //Continue Jumping as long as jump height/time hasn't exceeded
            if (Input.GetX() && isJumpingInAir && !isJumpButtonReleased)
            {
                System.Diagnostics.Debug.WriteLine("Continue Jumping");
                rigidBody.AddVelocity(rigidBody.Velocity.X, -jumpSpeed/50);
            }
        }

        void JumpAttack(GameTime gt)
        {
            //If get jump while is grounded, then jump and start timer
            if (Input.GetX_Down() && collider.isGrounded())
            {

                System.Diagnostics.Debug.WriteLine("Got jump button");
                rigidBody.SetGravityScale(40f);
                isJumpingInAir = true;
                isJumpButtonReleased = false;
                jumpTimeCounter = jumpTimeTotal;
                animator.Play(animations["Jump"]);
                rigidBody.SetVelocity(rigidBody.Velocity.X, -jumpSpeed);

                System.Diagnostics.Debug.WriteLine("Counting Timer, " + jumpTimeCounter + " seconds in progress");
                jumpTimeCounter -= (float)gt.ElapsedGameTime.TotalSeconds;
            }
            //If jump button is released or  the timer exceeds the limit, then stop jumping.
            if (Input.GetX_Up() || jumpTimeCounter >= jumpTimeTotal)
            {
                System.Diagnostics.Debug.WriteLine("Stop Jumping: Jump Button Up or Timer Exceeded");
                jumpTimeCounter = 0;
                isJumpButtonReleased = true;
                isJumpingInAir = false;
                //animator.Play(animations["Idle"]);
                //animator.Play(animations["Idle"]);
            }
            //If collides to ground after jumping, then stop jumping
            /*if (isJumpingInAir && collider.isGrounded() && jumpTimeCounter > 0.1f)
            {
                System.Diagnostics.Debug.WriteLine("Stop Jumping: Collided With Ground");
                jumpTimeCounter = 0;
                isJumpButtonReleased = true;
                isJumpingInAir = false;
            }*/
            //Continue Jumping as long as jump height/time hasn't exceeded
            if (Input.GetX() && isJumpingInAir && !isJumpButtonReleased)
            {
                System.Diagnostics.Debug.WriteLine("Continue Jumping");
                rigidBody.AddVelocity(rigidBody.Velocity.X, -jumpSpeed / 50);
            }
        }
    }
}

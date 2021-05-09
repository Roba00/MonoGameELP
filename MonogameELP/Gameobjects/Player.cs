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
        public override Transform transform { get => base.transform; set => base.transform = value; }
        private BoxCollider collider;
        private BoxCollider attackCollider;
        private BoxCollider attackUpCollider;
        private BoxCollider attackDownCollider;
        private Rigidbody rigidBody;
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private Dictionary<string, Animation> animations;

        private bool isJumping = false;
        private bool isJumpButtonReleased = true;
        private float jumpSpeed = 1000f; //750f //200f
        private float jumpTimeTotal = 0.5f; //0.45f //1f
        private float jumpTimeCounter;

        public Player()
        {
            transform = new Transform();
            jumpTimeCounter = jumpTimeTotal;
        }

        public void Initialize()
        {
            transform.Position = new Vector2(350f, 200f); //150f
            transform.Rotation = 0f;
            transform.Scale = new Vector2(8f, 8f);
        }

        public void LoadContent()
        {
            transform.Scale = new Vector2(8f, 8f);
            collider = new BoxCollider(transform, true, false, new Vector2(6, 8), "Player");
            //attackCollider = new BoxCollider(transform, false, true, "Attack");
            //attackUpCollider = new BoxCollider(transform, false, true, "AttackUp");
            //attackDownCollider = new BoxCollider(transform, false, true, "AttackDown");
            rigidBody = new Rigidbody(transform, collider, 40, 7.5f);
            spriteRenderer = new SpriteRenderer(Game1.plyrTxtr);
            CreateAnimations();
            animator = new Animator(animations["Idle"]);

            rigidBody.SetGravityScale(40f); //40f before testing
        }

        public void Update(GameTime gameTime)
        {
            Input.UpdateState();

            WalkControl();
            AttackControl();
            JumpControl(gameTime);
            JumpAttack(gameTime);

            rigidBody.Update(gameTime);
            collider.Update();
            animator.Update(gameTime);

            //System.Diagnostics.Debug.WriteLine("Player Position: (" + transform.Position.X + ", " + transform.Position.Y + ").");
            //System.Diagnostics.Debug.WriteLine("Player Velocity: " + rigidBody.Velocity.Y);
            //System.Diagnostics.Debug.WriteLine("Player Acceleration: " + rigidBody.Acceleration.Y);
            /*if (animator.GetState() == animations["Idle"])
            {
                System.Diagnostics.Debug.WriteLine("Player Anim State: Idle");
            }
            if (animator.GetState() == animations["Walk"])
            {
                System.Diagnostics.Debug.WriteLine("Player Anim State: Walk");
            }
            if (animator.GetState() == animations["Jump"])
            {
                System.Diagnostics.Debug.WriteLine("Player Anim State: Jump");
            }
            if (animator.GetState() == animations["JumpAttackUp"])
            {
                System.Diagnostics.Debug.WriteLine("Player Anim State: Jump Attack Up");
            }
            if (animator.GetState() == animations["Attack"])
            {
                System.Diagnostics.Debug.WriteLine("Player Anim State: Attack");
            }*/

        }

        public void Draw (SpriteBatch spriteBatch)
        {
            animator.Draw(spriteBatch, spriteRenderer.Texture, transform);
            /*Texture2D test = new Texture2D(spriteBatch.GraphicsDevice, 1, 1); // 1,1 
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
                { "Jump", new Animation(spriteRenderer.Texture, 9, 9, 18, 1f, true) },
                { "JumpAttackUp", new Animation(spriteRenderer.Texture, 10, 13, 18, 0.125f, false, 2) },
                { "JumpAttackDown", new Animation(spriteRenderer.Texture, 14, 17, 18, 0.125f, false) }
            };
        }

        void WalkControl()
        {
            if (Input.GetLeft() && animator.GetState() != animations["Attack"])
            {
                transform.Scale = new Vector2(-Math.Abs(transform.Scale.X), transform.Scale.Y);
                rigidBody.SetVelocity(-100, rigidBody.Velocity.Y);
                if (!(animator.GetState() == animations["Jump"] && !collider.IsGrounded())
                    || !(animator.GetState() == animations["JumpAttackUp"] && !collider.IsGrounded()))
                    animator.Play(animations["Walk"]);
            }
            else if (Input.GetRight() && animator.GetState() != animations["Attack"])
            {
                transform.Scale = new Vector2(Math.Abs(transform.Scale.X), transform.Scale.Y);
                rigidBody.SetVelocity(100, rigidBody.Velocity.Y);
                if (!(animator.GetState() == animations["Jump"] && !collider.IsGrounded())
                    || !(animator.GetState() == animations["JumpAttackUp"] && !collider.IsGrounded()))
                    animator.Play(animations["Walk"]);
            }
            else
            {
                rigidBody.SetVelocity(0, rigidBody.Velocity.Y);
                if (!(animator.GetState() == animations["Jump"] && !collider.IsGrounded())
                    || !(animator.GetState() == animations["JumpAttackUp"] && !collider.IsGrounded()))
                    animator.Play(animations["Idle"]);
            }
        }

        void AttackControl()
        {
            if (Input.GetZ_Down() && collider.IsGrounded())
            {
                animator.Play(animations["Attack"]);
            }
        }

        void JumpControl(GameTime gt)
        {
            if (!collider.IsGrounded() && animator.GetState() != animations["JumpAttackUp"])
            {
                animator.Play(animations["Jump"]);
            }
            //If get jump while is grounded, then jump and start timer
            if (Input.GetX_Down() && collider.IsGrounded() && animator.GetState() != animations["JumpAttackUp"])
            {
                System.Diagnostics.Debug.WriteLine("Got jump button");
                //rigidBody.SetGravityScale(40f);
                isJumping = true;
                isJumpButtonReleased = false;
                jumpTimeCounter = jumpTimeTotal;
                rigidBody.SetVelocity(rigidBody.Velocity.X, -jumpSpeed);

                System.Diagnostics.Debug.WriteLine("Counting Timer, " + jumpTimeCounter + " seconds in progress");
            }
            
            //If collides to ground after jumping, then stop jumping
            /*if (isJumping && collider.isGrounded() && jumpTimeCounter > 0.1f)
            {
                System.Diagnostics.Debug.WriteLine("Stop Jumping: Collided With Ground");
                jumpTimeCounter = 0;
                isJumpButtonReleased = true;
                isJumping = false;
            }*/

            //Continue Jumping as long as jump height/time hasn't exceeded
            if (Input.GetX() && isJumping && !isJumpButtonReleased && animator.GetState() != animations["JumpAttackUp"])
            {
                jumpTimeCounter -= (float)gt.ElapsedGameTime.TotalSeconds;
                System.Diagnostics.Debug.WriteLine("Continue Jumping, IsJumping " + isJumping + ", isJumpButtonReleased "
                    + isJumpButtonReleased + ", timer " + jumpTimeCounter);
                if (animator.GetState() != animations["JumpAttackUp"])
                    rigidBody.SetVelocity(rigidBody.Velocity.X, (float)(-jumpSpeed*(jumpTimeCounter+.01d))); // /50
            }

            //If jump button is released or  the timer exceeds the limit, then stop jumping.
            if (((Input.GetX_Up() || jumpTimeCounter <= 0) && isJumping) || animator.GetState() == animations["JumpAttackUp"])
            {
                System.Diagnostics.Debug.WriteLine("Stop Jumping: Jump Button Up or Timer Exceeded");
                if (animator.GetState() != animations["JumpAttackUp"])
                    rigidBody.SetVelocity(rigidBody.Velocity.X, 0);
                isJumping = false;
                isJumpButtonReleased = true;
                jumpTimeCounter = jumpTimeTotal;
                //animator.Play(animations["Idle"]);
            }
        }

        void JumpAttack(GameTime gt)
        {
            if (animator.GetState() == animations["Jump"] && Input.GetZ_Down() && Input.GetUp())
            {
                animator.Play(animations["JumpAttackUp"]);
                rigidBody.SetVelocity(rigidBody.Velocity.X, -300f);
                //System.Diagnostics.Debug.WriteLine("attack up air");
            }

            else if (animator.GetState() == animations["Jump"] && Input.GetZ_Down() && Input.GetDown())
            {
                animator.Play(animations["JumpAttackDown"]);
                rigidBody.SetVelocity(rigidBody.Velocity.X, 150f);
            }

            
        }
    }
}

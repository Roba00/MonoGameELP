using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonogameELP.Components
{
    public class Rigidbody
    {
        private Vector2 position;
        private float rotation;
        private Vector2 scale;

        private Transform transform;
        private BoxCollider collider;

        public Vector2 Velocity { get; private set; }
        public Vector2 Acceleration { get; private set; }

        public float Mass { get; private set; }

        //private readonly float Gravity = 9.81f;

        public float GravityScale { get; private set; }

        public Rigidbody(Transform transform, BoxCollider collider, float mass)
        {
            this.transform = transform;
            this.collider = collider;
            Mass = mass;
            GravityScale = 1;
        }

        public Rigidbody(Transform transform, BoxCollider collider, float mass, float gravityScale)
        {
            this.transform = transform;
            this.collider = collider;
            Mass = mass;
            GravityScale = gravityScale;
        }

        public void Update(GameTime gameTime)
        {
            position = transform.Position;
            rotation = transform.Rotation;
            scale = transform.Scale;

            Gravity(Mass);

            //Acceleration = new Vector2(Acceleration.X, Gravity * GravityScale);

            Velocity = new Vector2(Velocity.X + Acceleration.X * (float)gameTime.ElapsedGameTime.TotalSeconds, Velocity.Y + Acceleration.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);

            transform.Position = new Vector2((transform.Position.X + Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds),
                                              //+ Acceleration.X * (float)gameTime.ElapsedGameTime.TotalSeconds * (float)gameTime.ElapsedGameTime.TotalSeconds),
                                              (transform.Position.Y + Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds));
                                              //+ Acceleration.Y * (float)gameTime.ElapsedGameTime.TotalSeconds * (float)gameTime.ElapsedGameTime.TotalSeconds));

        }
        
        public void AddVelocity(float xVel, float yVel)
        {
            Velocity = new Vector2(Velocity.X + xVel, Velocity.Y + yVel);
        }

        public void SetVelocity(float xVel, float yVel)
        {
            Velocity = new Vector2(xVel, yVel);
        }

        public void AddAcceleration(float xAccel, float yAccel)
        {
            Acceleration = new Vector2(Acceleration.X + xAccel, Acceleration.Y + yAccel);
        }

        public void SetAcceleration(float xAccel, float yAccel)
        {
            Acceleration = new Vector2(xAccel, yAccel);
        }

        // Force = Mass * Acceleration
        // Acceleration = Force / Mass
        // Mass = Force / Acceleration
        public void AddForce(float xForce, float yForce)
        {
            AddAcceleration(xForce / Mass, yForce / Mass);
        }

        public void Gravity(float mass)
        {
            if (!collider.IsGrounded())
            {
                SetAcceleration(Acceleration.X, 9.81f * GravityScale);
            }
            else
            {
                if (Velocity.Y > 0)
                {
                    SetVelocity(Velocity.X, 0f);
                }
                if (Acceleration.Y > 0)
                {
                    SetAcceleration(Acceleration.X, 0f);
                }
            }
        }

        public void SetGravityScale(float gravityScale)
        {
            GravityScale = gravityScale;
        }
    }
}

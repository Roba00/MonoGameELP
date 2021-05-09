using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonogameELP;
using MonogameELP.Gameobjects;

namespace MonogameELP.Components
{
    public class BoxCollider
    {
        //private GameObject gameObject;
        private Transform tf;
        public Rectangle Rectangle { get; private set; }
        private Rectangle intersectionRectangle;
        private bool isCollision;
        private bool isGrounded;
        private bool isKinematic;
        private int colliderIndex;
        private bool isTrigger;
        private Vector2 scaleMultiplier;
        private String tag;
        public BoxCollider(Transform tf,bool isKinematic, bool isTrigger, String tag)
        {
            //this.gameObject = gameObject;
            this.tf = tf;

            Rectangle = new Rectangle((int)tf.Position.X, 
                (int)tf.Position.Y, (int)tf.Scale.X, 
                (int)tf.Scale.Y);
            Game1.colliders.Add(this);
            colliderIndex = Game1.colliders.IndexOf(this);
            this.isKinematic = isKinematic;
            this.isTrigger = isTrigger;
            this.scaleMultiplier = new Vector2(1, 1);
            this.tag = tag;
        }

        public BoxCollider(Transform tf, bool isKinematic, bool isTrigger, Vector2 scaleMultiplier,String tag)
        {
            //this.gameObject = gameObject;
            this.tf = tf;

            Rectangle = new Rectangle((int)(tf.Position.X-scaleMultiplier.X),
                (int)(tf.Position.Y-scaleMultiplier.Y), (int)(tf.Scale.X*scaleMultiplier.X),
                (int)(tf.Scale.Y * scaleMultiplier.Y));
            Game1.colliders.Add(this);
            colliderIndex = Game1.colliders.IndexOf(this);
            this.isKinematic = isKinematic;
            this.isTrigger = isTrigger;
            this.scaleMultiplier = scaleMultiplier;
            this.tag = tag;
        }

        public BoxCollider(Transform tf, bool isKinematic, bool isTrigger)
        {
            //this.gameObject = gameObject;
            this.tf = tf;

            Rectangle = new Rectangle((int)tf.Position.X,
                (int)tf.Position.Y, (int)tf.Scale.X,
                (int)tf.Scale.Y);
            Game1.colliders.Add(this);
            colliderIndex = Game1.colliders.IndexOf(this);
            this.isKinematic = isKinematic;
            this.isTrigger = isTrigger;
            this.scaleMultiplier = new Vector2(1, 1);
            this.tag = null;
        }

        public void Update()
        {
            if (isKinematic)
            {
                if (scaleMultiplier.Y == 1)
                {
                    Rectangle = new Rectangle((int)tf.Position.X,
                    (int)tf.Position.Y, (int)tf.Scale.X,
                    (int)tf.Scale.Y);
                }
                else //The player's collider
                {
                    if (tf.Scale.X > 0)
                    {
                        Rectangle = new Rectangle((int)(tf.Position.X - 30),
                        (int)(tf.Position.Y - 57), Math.Abs((int)(tf.Scale.X * scaleMultiplier.X)),
                        (int)(tf.Scale.Y * scaleMultiplier.Y));
                    }
                    else
                    {
                        Rectangle = new Rectangle((int)(tf.Position.X - 10),
                        (int)(tf.Position.Y - 57), Math.Abs((int)(tf.Scale.X * scaleMultiplier.X)),
                        (int)(tf.Scale.Y * scaleMultiplier.Y));
                    }
                }
            }

            if (isKinematic)
            {
                CheckCollisions();
            }
        }

        public void CheckCollisions()
        {
            isCollision = false;
            isGrounded = false;

            for (int i=0; i < Game1.colliders.Count; i++)
            {
                if (i != colliderIndex) //(Game1.colliders[i].Rectangle != Rectangle)
                {
                    if (Rectangle.Intersects(Game1.colliders[i].Rectangle))
                    {
                        //System.Diagnostics.Debug.WriteLine("Collision 1! Index " + colliderIndex + " collided with " + i);
                        Rectangle otherRect = Game1.colliders[i].Rectangle;
                        isCollision = true;
                        ReactCollision(otherRect);
                    }
                }
            }
        }

        public void ReactCollision(Rectangle otherRect)
        {
            intersectionRectangle = Rectangle.Intersect(Rectangle, otherRect);

            if (Rectangle.Top <= otherRect.Bottom)
            {
                isGrounded = true;
            }


            if (intersectionRectangle.Height > 1f //Prevents jittering
                            && intersectionRectangle.Height < 3f
                            && Rectangle.Bottom > otherRect.Top
                            && Rectangle.Bottom < otherRect.Bottom)  //Prevents translating when intersected through X axis
            {
                tf.Translate(0, -intersectionRectangle.Height);
            }
            if (intersectionRectangle.Width > 0f && intersectionRectangle.Width < 3f && Math.Abs(Rectangle.Bottom - otherRect.Top) > 0f)
            {
                if (Rectangle.Left > otherRect.Left)
                {
                    tf.Translate(intersectionRectangle.Width, 0);
                }
                if (Rectangle.Right < otherRect.Right)
                {
                    tf.Translate(-intersectionRectangle.Width, 0);
                }
            }
        }

        /*public bool IsGrounded()
        {
            for (int i = 0; i < Game1.colliders.Count; i++)
            {
                if (Rectangle.Intersects(Game1.colliders[i].Rectangle))
                {
                    //If the intersecting gameobject contains the bottom point of the player
                    if (Game1.colliders[i].Rectangle.Contains(Rectangle.Right-Rectangle.Width, Rectangle.Bottom))
                    {
                        //The player is grounded
                        return true;
                    }
                }
            }
            return false;
        }*/

        public bool IsGrounded()
        {
            return isGrounded;
        }
    }
}

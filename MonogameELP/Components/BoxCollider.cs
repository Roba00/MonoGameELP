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
        private bool isKinematic;
        private int colliderIndex;
        public BoxCollider(Transform tf,bool isKinematic)
        {
            //this.gameObject = gameObject;
            this.tf = tf;

            Rectangle = new Rectangle((int)tf.Position.X, 
                (int)tf.Position.Y, (int)tf.Scale.X, 
                (int)tf.Scale.Y);
            Game1.colliders.Add(this);
            colliderIndex = Game1.colliders.IndexOf(this);
            this.isKinematic = isKinematic;
        }

        public void Update()
        {
            //Rectangle.Offset(tf.Position.X, tf.Position.Y);
            Rectangle = new Rectangle((int)tf.Position.X,
                (int)tf.Position.Y, (int)tf.Scale.X,
                (int)tf.Scale.Y);
            /*if (colliderIndex == 0)
            {
                System.Diagnostics.Debug.WriteLine("Player Y Box: " + tf.Position.Y);
                System.Diagnostics.Debug.WriteLine("Rectangle Y: " + Rectangle.Location.Y);
            }*/
            if (isKinematic)
            {
                CheckCollisions();
                ReactCollision();
            }
        }

        public void CheckCollisions()
        {
            //Rectangle lowerRectangle = new Rectangle(Rectangle.X, Rec);
            isCollision = false;

            for (int i=0; i < Game1.colliders.Count; i++)
            {
                if (i != colliderIndex) //(Game1.colliders[i].Rectangle != Rectangle)
                {
                    if (Rectangle.Intersects(Game1.colliders[i].Rectangle))
                    {
                        System.Diagnostics.Debug.WriteLine("Collision 1! Index " + colliderIndex + " collided with " + i);
                        intersectionRectangle = Rectangle.Intersect(Rectangle, Game1.colliders[i].Rectangle);
                        isCollision = true;
                    }
                }
            }
        }

        public void ReactCollision()
        {
            if (isCollision)
            {
                if (intersectionRectangle.Height > 1f) //Prevents jittering
                {
                    tf.Translate(0, -intersectionRectangle.Height);
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

        public bool isGrounded()
        {
            return isCollision;
        }
    }
}

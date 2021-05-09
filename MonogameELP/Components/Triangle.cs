using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonogameELP.Components
{
    public class Triangle : IEquatable<Triangle>
    {
        public Point topPoint;
        public Point bottomPoint;
        public Point sidePoint;
        public double theta;

        public Triangle(Point topPoint, Point bottomPoint, Point sidePoint, double theta)
        {
            this.topPoint = topPoint;
            this.bottomPoint = bottomPoint;
            this.sidePoint = sidePoint;
            this.theta = theta;
        }

        public bool IsIntersection(Rectangle rectangle)
        {
            Point intersectionPoint = new Point(rectangle.Center.X, (int) Math.Tan(theta)*(rectangle.Center.X-bottomPoint.X));
            if (bottomPoint.X < intersectionPoint.X && intersectionPoint.X < intersectionPoint.Y)
            {
                if (rectangle.Y < intersectionPoint.Y)
                {
                    return true;
                }
            }
            return false;
        }

        public int IntersectionDepth(Rectangle rectangle)
        {
            Point intersectionPoint = new Point(rectangle.Center.X, (int)Math.Tan(theta) * (rectangle.Center.X - bottomPoint.X));
            if (bottomPoint.X < intersectionPoint.X && intersectionPoint.X < intersectionPoint.Y)
            {
                if (rectangle.Y < intersectionPoint.Y)
                {
                    return rectangle.Y - intersectionPoint.Y;
                }
            }
            return 0;
        }

        public bool Equals(Triangle other)
        {
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Triangle personObj = obj as Triangle;
            if (personObj == null)
                return false;
            else
                return Equals(personObj);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}

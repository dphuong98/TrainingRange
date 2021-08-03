using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public struct Rectangle : IShape
    {
        private Point topRight;
        private float width;
        private float height;

        public Rectangle(Point topRight, float width, float height)
        {
            this.topRight = topRight;
            this.width = width;
            this.height = height;
        }

        public bool Contains(Point p)
        {
            throw new System.NotImplementedException();
        }

        public float Area()
        {
            throw new System.NotImplementedException();
        }

        public bool Intersect(ILine line, ref List<Point> intersections)
        {
            throw new System.NotImplementedException();
        }


        public Rectangle AABB()
        {
            throw new System.NotImplementedException();
        }
    }
}
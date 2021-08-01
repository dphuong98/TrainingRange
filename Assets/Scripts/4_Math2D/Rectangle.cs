using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public class Rectangle : IShape
    {
        private Point topLeft;
        private float width;
        private float height;

        public Rectangle(Point topLeft, float width, float height)
        {
            this.topLeft = topLeft;
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

        public bool Intersect(Line line, ref List<Point> intersections)
        {
            throw new System.NotImplementedException();
        }

        public bool Intersect(LineSegment line, ref List<Point> intersections)
        {
            throw new System.NotImplementedException();
        }

        public Rectangle AABB()
        {
            throw new System.NotImplementedException();
        }
    }
}
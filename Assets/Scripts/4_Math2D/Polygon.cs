using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Math2D
{
    public struct Polygon : IShape
    {
        private List<Point> points;

        public Polygon(List<Point> points)
        {
            if (points.Count() < 3)
                throw new ArgumentException("A polygon must have at least 3 points");
            this.points = points;
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
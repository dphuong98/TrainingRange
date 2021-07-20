using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public class LineSegment
    {
        public Point a;
        public Point b;

        public LineSegment(Point a, Point b)
        {
            this.a = a;
            this.b = b;
        }

        public float Length => Point.Distance(a, b);

        public bool Contains(Point p)
        {
            throw new NotImplementedException();
        }

        public bool Intersect(LineSegment other, ref Point intersection)
        {
            throw new NotImplementedException();
        }

        public bool Intersect(Line line, ref Point intersection)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public struct Line : ILine
    {
        private float _a;
        private float _b;
        private float _c;
        public float a
        {
            get => _a;
            set
            {
                _a = value;
                CheckValid();
            }
        }
        public float b
        {
            get => _b;
            set
            {
                _b = value;
                CheckValid();
            }
        }

        public float c
        {
            get => _c;
            set => _c = value;
        }
        
        public Line(float a, float b, float c)
        {
            _a = a;
            _b = b;
            _c = c;
            
            CheckValid();
        }

        public Line(Point a, Point b)
        {
            var slope = (b.y - a.y) / (b.x - a.x);

            _a = -slope;
            _b = 1;
            _c = -a.x * slope + a.y;
            
            CheckValid();
        }

        private void CheckValid()
        {
            if (a.NearlyEqual(b) && a.NearlyEqual(0))
                throw new ArgumentException("a and b of a line cannot both be 0!");
        }

        // Find intersection of two line, return `false` if not intersect
        public bool Intersect(Line other, ref Point intersection)
        {
            //https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line
            var delta = a * other.b - other.a * b;

            if (delta.NearlyEqual(0)) return false;

            intersection = new Point( (other.b * c - b * other.c) / delta, (a * other.c - other.a * c) / delta );
            return true;
        }

        public bool Contains(Point p)
        {
            return (a * p.x + b * p.y).NearlyEqual(c);
        }

        // Find projection of the point `p` on this line 
        public Point Project(Point p)
        {
            //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
            if (Contains(p)) return p;

            return new Point(
                (b * ( b*p.x - a*p.y) + a*c) / (a*a+b*b),
                (a * (-b*p.x + a*p.y) + b*c) / (a*a+b*b) );
        }
        
        public int Side(Point p)
        {
            var dif = a * p.x + b * p.y - c;
            if (dif.NearlyEqual(0))
            {
                return 0;
            }
            if (dif < 0) return -1;
            return 1;
        }

        // Calculate distance from point `p` to this line
        public float Distance(Point p)
        {
            //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
            return (float)(Math.Abs(a * p.x + b * p.y - c) / Math.Sqrt(a * a + b * b));
        }

        public override string ToString()
        {
            return a + "x + " + b + "y = " + c;
        }

        public bool Intersect(ILine other, ref Point intersection)
        {
            if (other is LineSegment segment)
            {
                return segment.Intersect(this, ref intersection);
            }

            if (other is Line line)
            {
                return line.Intersect(this, ref intersection);
            }

            return false;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public class LineSegment : Line
    {
        public new Point a;
        public new Point b;

        public LineSegment(Point a, Point b) : base(a, b)
        {
            this.a = a;
            this.b = b;
        }

        public float Length => Point.Distance(a, b);

        public bool Contains(Point p)
        {
            //https://stackoverflow.com/questions/328107/how-can-you-determine-a-point-is-between-two-other-points-on-a-line-segment
            var ba = b - a;
            var pa = p - a;
            
            //Check alignment a, b, p
            if (!Point.Cross(ba, pa).NearlyEqual(0)) return false;
            
            //Check if ba is opposite to pa
            //When pa and ba points in the same direction, check if pa > ba in length
            return !(ba * pa < 0) && !(ba * pa > ba.Length * ba.Length);
        }

        public bool Intersect(LineSegment other, ref Point intersection)
        {
            //Alternative: https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line_segment
            return base.Intersect(other, ref intersection) && Contains(intersection) && other.Contains(intersection);
        }

        public bool Intersect(Line line, ref Point intersection)
        {
            //Alternative: Check if line is strictly one one side of the line
            return base.Intersect(line, ref intersection) && Contains(intersection);
        }

        public override string ToString()
        {
            return a +" -> " + b;
        }
    }
}
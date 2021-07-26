using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public struct LineSegment
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
            //https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line_segment
            var vector1 = new Point(a.x - b.x, a.y - b.y);
            var vector2 = new Point(other.a.x - other.b.x, other.a.y - other.b.y);

            if (Point.Cross(vector1, vector2).NearlyEqual(0)) //Check parallel
            {
                return false;
            }
            
            var d = (a.x - b.x) * (other.a.y - other.b.y) - (a.y - b.y) * (other.a.x - other.b.x);
            
            var t = ((a.x - other.a.x) * (other.a.y - other.b.y) - (a.y - other.a.y) * (other.a.x - other.b.x)) / d;
            var u = ((b.x - a.x) * (a.y - other.a.y) - (b.y - a.y) * (a.x - other.a.x)) / d;

            if (0 <= t && t <= 1 && 0 <= u && u <= 1)
            {
                intersection = new Point(a.x + t * (b.x - a.x), a.y + t * (b.y - a.y));
                return true;
            }

            return false;
        }

        public bool Intersect(Line line, ref Point intersection)
        {
            //Check if segment coincident with line
            if (line.Contains(a) && line.Contains(b))
                return false;

            if (line.Contains(a))
            {
                intersection = a;
                return true;
            }
            
            if (line.Contains(b))
            {
                intersection = b;
                return true;
            }
            
            if (line.Side(a) == line.Side(b))
                return false;
            
            var lineThroughSegment = new Line(a, b);
            return lineThroughSegment.Intersect(line, ref intersection);
        }

        public override string ToString()
        {
            return a +" -> " + b;
        }
    }
}
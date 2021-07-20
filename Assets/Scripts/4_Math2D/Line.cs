using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public class Line
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
        public float c { get; set; }
        
        public Line(float a, float b, float c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            
            CheckValid();
        }

        public void CheckValid()
        {
            if (a == b && a == 0)
                throw new ArgumentException("a and b of a line cannot both be 0!");
        }

        // Find intersection of two line, return `false` if not intersect
        public bool Intersect(Line other, ref Point intersection)
        {
            var delta = a * other.b - other.a * b;

            if (delta == 0) return false;

            intersection = new Point( (other.b * c - b * other.c) / delta, (a * other.c - other.a * c) / delta );
            return true;
        }

        public bool Contains(Point p)
        {
            return a * p.x + b * p.y == c;
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

        // Calculate distance from point `p` to this line
        public float Distance(Point p)
        {
            //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
            return (float)(Math.Abs(a * p.x + b * p.y - c) / Math.Sqrt(a * a + b * b));
        }
    }
}
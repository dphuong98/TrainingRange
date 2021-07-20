using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public struct Point
    {
        public float x;
        public float y;

        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    
        public float Length => Distance(this, zero);
        public Point Normalized
        {
            get
            {
                var copy = this;
                copy.Normalize();
                return new Point(copy.x, copy.y);
            }
        }

        public void Normalize()
        {
            var length = Length;
            x /= length;
            y /= length;
        }

        public static float Distance(Point a, Point b)
        {
            return (float)Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y);
        }

        // Dot Product
        public static float operator *(Point a, Point b)
        {
            return a.x * b.x + a.y * b.y;
        }

        // Cross Product
        public static float Cross(Point a, Point b)
        {
            return a.x * b.y - a.y * b.x;
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        private static readonly Point zeroPoint = new Point(0, 0);
    
        public static Point zero { get { return zeroPoint; }
        }
    }
}
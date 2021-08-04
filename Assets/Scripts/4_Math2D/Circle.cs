using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public struct Circle : IShape
    {
        public Point center;
        public float radius;

        public Circle(Point center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public bool Contains(Point p)
        {
            return (p.x - center.x) * (p.x - center.x) + (p.y - center.y) * (p.y - center.y) < radius * radius;
        }

        public float Area()
        {
            return (float)(Math.PI * radius * radius);
        }

        public bool Intersect(ILine line, ref List<Point> intersections)
        {
            Line lineData;
            if (line is LineSegment)
            {
                var segment = (LineSegment) line;
                lineData = new Line(segment.a, segment.b);
            }
            else lineData = (Line) line;

            var projection = line.Project(center);
            
            var d = Point.Distance(projection, center);
            if (d > radius) return false;

            var v = (float)Math.Sqrt(radius * radius - d * d);
            if (v.NearlyEqual(0))
            {
                //Line is a tangent for circle
                intersections.Add(projection);
                return true;
            }
            
            //Line cut circle at 2 points
            var offsetVector = new Point(-lineData.b, lineData.a).Normalized;
            offsetVector.x *= v;
            offsetVector.y *= v;
            intersections.Add(new Point(projection.x + offsetVector.x, projection.y + offsetVector.y));
            intersections.Add(new Point(projection.x - offsetVector.x, projection.y - offsetVector.y));
            return true;
        }

        public Rectangle AABB()
        {
            return new Rectangle(new Point(center.x + radius, center.y + radius), radius * 2, radius * 2);
        }
    }
}
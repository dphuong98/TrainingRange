using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Math2D
{
    public struct Polygon : IShape
    {
        private readonly Point[] vertices;

        public Polygon(Point[] vertices)
        {
            if (vertices.Length < 3)
                throw new ArgumentException("A polygon must have at least 3 points");
            this.vertices = vertices;
        }

        public bool Contains(Point p)
        {
            var isInside = false;
            int i, j;
            for (i = 0, j = vertices.Length - 1; i < vertices.Length; j = i++)
            {
                var pi = vertices[i];
                var pj = vertices[j];
                if (((pi.y <= p.y && p.y < pj.y) || (pj.y <= p.y && p.y < pi.y)) &&
                    (p.x < (pj.x - pi.x) * (p.y - pi.y) / (pj.y - pi.y) + pi.x))
                    isInside = !isInside;
            }

            return isInside;
        }

        public float Area()
        {
            //Shoelace formula may not account for polygon with crossing segments
            
            // Initialize area
            var area = 0f;
            var n = vertices.Length;
            int i, j;
            
            // Calculate value of shoelace formula
            for (i = 0, j = n - 1; i < n; j = i++) {
                area += (vertices[j].x + vertices[i].x) * (vertices[j].y - vertices[i].y);
            }
 
            // Return absolute value
            return Math.Abs(area / 2f);
        }

        public bool Intersect(ILine line, ref List<Point> intersections)
        {
            var segments = vertices.Zip(vertices.Skip(1), (a, b) => new LineSegment(a, b)).ToList();
            segments.Add(new LineSegment(vertices.Last(), vertices.First()));
            foreach (var segment in segments)
            {
                var intersection = new Point();
                if (line.Intersect(segment, ref intersection))
                {
                    intersections.Add(intersection);
                }
            }

            return intersections.Count > 0;
        }

        public Rectangle AABB()
        {
            var xMin = vertices.Min(v => v.x);
            var xMax = vertices.Max(v => v.x);
            var yMin = vertices.Min(v => v.y);
            var yMax = vertices.Max(v => v.y);

            return new Rectangle(new Point(xMax, yMax), xMax - xMin, yMax - yMin);
        }
    }
}
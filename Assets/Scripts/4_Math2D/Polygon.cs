using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Math2D
{
    public struct Polygon : IShape
    {
        private readonly List<Point> vertices;

        public Polygon(List<Point> vertices)
        {
            if (vertices.Count < 3)
                throw new ArgumentException("A polygon must have at least 3 points");
            this.vertices = new List<Point>(vertices);
        }

        public bool Contains(Point p)
        {
            var isInside = false;
            int i, j;
            for (i = 0, j = vertices.Count - 1; i < vertices.Count; j = i++)
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
            var n = vertices.Count;
            int i, j;
            
            // Calculate value of shoelace formula
            for (i = 0, j = n - 1; i < n; j = i++) {
                area += (vertices[j].x + vertices[i].x) * (vertices[j].y - vertices[i].y);
            }
 
            // Return absolute value
            return Math.Abs(area / 2f);
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
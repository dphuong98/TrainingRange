using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Math2D
{
    public struct Polygon : IShape
    {
        private List<Point> vertices;

        public Polygon(List<Point> vertices)
        {
            if (vertices.Count() < 3)
                throw new ArgumentException("A polygon must have at least 3 points");
            this.vertices = vertices;
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
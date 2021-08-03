using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Math2D
{
    public struct Rectangle : IShape
    {
        private Point topRight;
        private float width;
        private float height;

        public Rectangle(Point topRight, float width, float height)
        {
            this.topRight = topRight;
            this.width = width;
            this.height = height;
        }

        public bool Contains(Point p)
        {
            return (topRight.x - width < p.x && p.x < topRight.x) && (topRight.y - height < p.y &&  p.y < topRight.y);
        }

        public float Area()
        {
            return width * height;
        }

        public bool Intersect(ILine line, ref List<Point> intersections)
        {
            var segments = GetSegments();
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

        public List<LineSegment> GetSegments()
        {
            var vertices = new Point[] { topRight, new Point(topRight.x, topRight.y - height),
                new Point(topRight.x - width, topRight.y - height), new Point(topRight.x - width, topRight.y) };
            var segments = vertices.Zip(vertices.Skip(1), (a, b) => new LineSegment(a, b)).ToList();
            segments.Add(new LineSegment(vertices.Last(), vertices.First()));
            return segments;
        }

        public Rectangle AABB()
        {
            return new Rectangle(topRight, width, height);
        }
    }
}
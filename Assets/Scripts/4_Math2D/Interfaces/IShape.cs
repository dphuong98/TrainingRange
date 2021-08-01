using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public interface IShape
    {
        bool Contains(Point p);
        float Area();
        bool Intersect(Line line, ref List<Point> intersections);
        bool Intersect(LineSegment line, ref List<Point> intersections);
        Rectangle AABB();
    }
}
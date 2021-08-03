using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public interface IShape
    {
        bool Contains(Point p);
        float Area();
        bool Intersect(ILine line, ref List<Point> intersections);
        Rectangle AABB();
    }
}
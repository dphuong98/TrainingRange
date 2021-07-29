using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public interface ILine
    {
        Point Project(Point p);
        bool Intersect(ILine other, ref Point intersection);
    }
}
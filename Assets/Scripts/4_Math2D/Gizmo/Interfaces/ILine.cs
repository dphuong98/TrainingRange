using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public interface ILine
    {
        bool Intersect(ILine other, ref Point intersection);
    }
}
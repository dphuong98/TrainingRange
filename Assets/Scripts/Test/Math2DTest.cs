using System;
using System.Collections;
using System.Collections.Generic;
using Math2D;
using UnityEngine;

public class Math2DTest : MonoBehaviour
{
    private void Start()
    {
        var pt0 = Point.zero;
        var pt1 = new Point(2, 3);
        var pt2 = new Point(-3, 4);
        var pt3 = new Point(0, -2);

        var nor1 = pt1.Normalized;
        var nor2 = pt2.Normalized;
        var nor3 = pt3.Normalized;

        var dis1 = Point.Distance(pt1, pt2);
        var dis2 = Point.Distance(pt2, pt3);

        var sum1 = pt1 + pt2;
        var sum2 = pt2 + pt3;

        var dif1 = pt1 - pt2;
        var dif2 = pt2 - pt3;

        var mag1 = pt1.Length;
        var mag2 = pt2.Length;
        var mag3 = pt3.Length;

        var dot1 = pt1 * pt2;
        var dot2 = pt2 * pt3;
        var dot3 = pt1 * pt3;

        var crs1 = Point.Cross(pt1, pt2);
        var crs2 = Point.Cross(pt2, pt3);
        var crs3 = Point.Cross(pt1, pt3);
        
        var done = new bool();
    }
}

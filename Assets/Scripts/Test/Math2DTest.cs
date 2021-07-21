using System;
using System.Collections;
using System.Collections.Generic;
using Math2D;
using UnityEngine;

public class Math2DTest : MonoBehaviour
{
    private void Start()
    {
        //PointTest();
        //LineTest();
        LineSegmentTest();
    }

    void PointTest()
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

    void LineTest()
    {
        var ln1 = new Line(1, 2, 3);
        var ln2 = new Line(1, 2, 2);
        var ln3 = new Line(2, 0, 2);

        Point intersection1 = new Point();
        var in1 = ln1.Intersect(ln2, ref intersection1);
        Point intersection2 = new Point();
        var in2 = ln2.Intersect(ln3, ref intersection2);

        var con1 = ln1.Contains(new Point(0, 0));
        var con2 = ln2.Contains(new Point(4, -1));

        var prj1 = ln1.Project(new Point(3, 4));
        var prj2 = ln3.Project(new Point(3, 4));

        var dis1 = ln1.Distance(new Point(3, 4));
        var dis2 = ln3.Distance(new Point(3, 4));

        try
        {
            var err1 = new Line(1, 0, 5);
            err1.a = 0;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        
        var done = new bool();
    }

    void LineSegmentTest()
    {
        var ln1 = new LineSegment(new Point(1, 2), new Point(2, 3));
        var ln2 = new LineSegment(new Point(1, 3), new Point(2, 2));
        var ln3 = new LineSegment(new Point(-1, -1), new Point(-2, 0));
        var ln4 = new LineSegment(new Point(2, 2), new Point(3, 3));

        var con1 = ln1.Contains(new Point(1.465f, 2.465f));
        var con2 = ln1.Contains(new Point(3f, 4f));
        var con3 = ln1.Contains(new Point(4f, 4f));

        var pt1 = new Point();
        var in1 = ln1.Intersect(ln2, ref pt1);
        var pt2 = new Point();
        var in2 = ln1.Intersect(ln3, ref pt2);
        var pt3 = new Point();
        var in3 = ln1.Intersect(ln4, ref pt3);

        var pt4 = new Point();
        var in4 = ln1.Intersect(new Line(1, 1, 4), ref pt4);
        var pt5 = new Point();
        var in5 = ln1.Intersect(new Line(1, 1, 1), ref pt5);
        
        var done = new Point();
    }
}

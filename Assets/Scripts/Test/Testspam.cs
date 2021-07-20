using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Math2D;
using UnityEngine;

public class Testspam : MonoBehaviour
{
    [ContextMenu("DoTest")]
    // Update is called once per frame
    private void DoTest()
    {
        var pt1 = new Point();
        var pt2 = new Point(3, 4);
        Debug.Log(Point.Distance(pt1, pt2));
        Debug.Log(new Point(3, 2) + new Point(1, 2));
    }
}

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
        var ft = 24.3f;
        var com = 24.3f.CompareTo(ft);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Testspam : MonoBehaviour
{
    [ContextMenu("DoTest")]
    // Update is called once per frame
    private void DoTest()
    {
        var type = typeof(TankController);
        var tmp = type.GetFields().Select(f=> f.GetRequiredCustomModifiers().Length);
        foreach (var VARIABLE in tmp)
        {
            Debug.Log(VARIABLE);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGizmo : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    public Color segmentColor  = Color.green;

    private void OnDrawGizmos()
    {
        if (point1 == null || point2 == null)
        {
            UnityEditor.EditorApplication.delayCall+= () => DestroyImmediate(gameObject);
        }
        
        var start = point1.transform.position;
        var end = point2.transform.position;

        Gizmos.color = segmentColor;
        Gizmos.DrawLine(start, end);
    }
}

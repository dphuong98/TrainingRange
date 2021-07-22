using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Axis : MonoBehaviour
{
    public enum AxisType
    {
        X,
        Y,
        Z
    }

    public Color AxisColor = Color.black;
    public AxisType SelectedAxis;
    public int Length;

    private void OnDrawGizmos()
    {
        Vector3 start = new Vector3();
        Vector3 end = new Vector3();
        switch (SelectedAxis)
        {
            case AxisType.X:
                start.x = Length;
                end.x = -Length;
                break;
            case AxisType.Y:
                start.y = Length;
                end.y = -Length;
                break;
            case AxisType.Z:
                start.z = Length;
                end.z = -Length;
                break;
        }

        var thickness = (Vector3)(Vector2.Perpendicular(start - end).normalized * 1/50);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(start, end);
        Gizmos.DrawLine(start + thickness, end + thickness);
        Gizmos.DrawLine(start - thickness, end - thickness);
    }
}

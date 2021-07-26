using System;
using System.Collections;
using System.Collections.Generic;
using Math2D;
using UnityEditor;
using UnityEngine;

public class SegmentGizmo : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    public Color segmentColor  = Color.green;
    public Vector3 TextSpacing = Vector3.right * 0.15f;

    private Vector3 start;
    private Vector3 end;

    private void OnDrawGizmos()
    {
        if (point1 == null || point2 == null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        
        start = point1.transform.position;
        end = point2.transform.position;

        Gizmos.color = segmentColor;
        Gizmos.DrawLine(start, end);

        if (Selection.activeTransform == transform)
        {
            var segmentLength = Point.Distance(new Point(start.x, start.y), new Point(end.x, end.y));
            Handles.Label((start + end) / 2 + TextSpacing, segmentLength.ToString(), EditorStyles.boldLabel);
        }
    }

    public LineSegment GetLineSegment()
    {
        return new LineSegment(new Point(start.x, start.y), new Point(end.x, end.y));
    }
}

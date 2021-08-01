using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Math2D
{
    public class SegmentGizmo : MonoBehaviour
    {
        public Transform point1;
        public Transform point2;
        
        public Color segmentColor  = Color.green;
        public Vector3 TextSpacing = Vector3.right * 0.15f;

        private void OnDrawGizmos()
        {
            if ((point1 == null || point2 == null) && !gameObject.IsPrefab())
            {
                DestroyImmediate(gameObject);
                return;
            }
        
            var start = point1.position;
            var end = point2.position;

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
            var start = point1.position;
            var end = point2.position;
        
            return new LineSegment(new Point(start.x, start.y), new Point(end.x, end.y));
        }
    }
}
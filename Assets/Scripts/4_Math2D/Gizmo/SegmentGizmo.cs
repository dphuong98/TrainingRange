using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Math2D
{
    public class SegmentGizmo : MonoBehaviour
    {
        public GameObject point1;
        public GameObject point2;
        public Color segmentColor  = Color.green;
        public Vector3 TextSpacing = Vector3.right * 0.15f;

        private void OnDrawGizmos()
        {
            if (point1 == null || point2 == null)
            {
                if (gameObject.IsPrefab()) return;
            
                DestroyImmediate(gameObject);
                return;
            }
        
            var start = point1.transform.position;
            var end = point2.transform.position;

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
            var start = point1.transform.position;
            var end = point2.transform.position;
        
            return new LineSegment(new Point(start.x, start.y), new Point(end.x, end.y));
        }
    }
}
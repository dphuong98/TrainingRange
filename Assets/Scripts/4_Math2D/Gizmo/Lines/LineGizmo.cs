using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public class LineGizmo : MonoBehaviour
    {
        public Transform point1;
        public Transform point2;

        public Color lineColor = Color.blue;

        private void OnDrawGizmos()
        {
            if ((point1 == null || point2 == null) && !gameObject.IsPrefab())
            {
                DestroyImmediate(gameObject);
                return;
            }
        
            var start = point1.position;
            var end = point2.position;

            var point = (start + end) / 2;
            var direction = (start - end) * 20f;
        
            Gizmos.color = lineColor;
            Gizmos.DrawRay(new Vector3(point.x, point.y, 0), direction);
            Gizmos.DrawRay(new Vector3(point.x, point.y, 0), -direction);
        }

        public Line GetLine()
        {
            var start = point1.position;
            var end = point2.position;
        
            return new Line(new Point(start.x, start.y), new Point(end.x, end.y));;
        }
    }
}
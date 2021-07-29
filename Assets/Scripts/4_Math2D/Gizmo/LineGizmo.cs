using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public class LineGizmo : MonoBehaviour
    {
        public GameObject point1;
        public GameObject point2;

        public Color lineColor = Color.blue;

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

            var point = (start + end) / 2;
            var direction = (start - end) * 20f;
        
            Gizmos.color = lineColor;
            Gizmos.DrawRay(new Vector3(point.x, point.y, 0), direction);
            Gizmos.DrawRay(new Vector3(point.x, point.y, 0), -direction);
        }

        public Line GetLine()
        {
            var start = point1.transform.position;
            var end = point2.transform.position;
        
            return new Line(new Point(start.x, start.y), new Point(end.x, end.y));;
        }
    }
}
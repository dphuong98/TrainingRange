using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Math2D
{
    public class CircleGizmo : MonoBehaviour, IShapeGizmo
    {
        public Transform center;
        public float radius;
        
        public Color boundingBoxColor = Color.cyan;
        public Color circleColor = Color.yellow;

        private void OnDrawGizmos()
        {
            if (center == null)
            {
                DestroyImmediate(gameObject);
                return;
            }

            RenderShape();
            
            if (Selection.activeTransform == transform)
            {
                RenderArea();
                RenderBoundingBox();
            }
        }

        private void RenderShape()
        {
            Handles.color = circleColor;
            Handles.DrawWireDisc(center.position // position
                , Vector3.back // normal
                , radius); // radius
        }

        private void RenderBoundingBox()
        {
            var boundingBox = GetCircle().AABB();
            var topRight = new Vector3(boundingBox.topRight.x, boundingBox.topRight.y, 0);
            var width = boundingBox.width;
            var height = boundingBox.height;
            var verticesPoint = new Vector3[]
            {
                topRight,
                topRight + new Vector3(0, -height, 0),
                topRight + new Vector3(-width, -height, 0),
                topRight + new Vector3(-width, 0, 0),
            };
        
            Gizmos.color = boundingBoxColor;
            for (var i = 0; i < verticesPoint.Length - 1; i++)
            {
                Gizmos.DrawLine(verticesPoint[i], verticesPoint[i+1]);
            }
            Gizmos.DrawLine(verticesPoint[verticesPoint.Length - 1], verticesPoint[0]);
        }

        private void RenderArea()
        {
            if (Selection.activeTransform == transform)
            {
                var circle = GetCircle();
                var text = "Area: " + circle.Area();
                Handles.Label(center.position + new Vector3(-0.5f, -0.5f), text, EditorStyles.boldLabel);
            }
        }

        public Circle GetCircle()
        {
            return new Circle(new Point(center.position.x, center.position.y), radius);
        }
    }
}
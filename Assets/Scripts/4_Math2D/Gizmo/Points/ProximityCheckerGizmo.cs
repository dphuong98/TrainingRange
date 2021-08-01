using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Math2D
{
    public class ProximityCheckerGizmo : MonoBehaviour
    {
        public Transform shape;

        private void OnDrawGizmos()
        {
            var shapeData = shape.GetShape();
            if (!shapeData.Contains(new Point(transform.position.x, transform.position.y))) return;
            
            var text = "Inside " + shape.name;
            Handles.Label(transform.position + new Vector3(-0.25f, -0.35f), text, EditorStyles.boldLabel);
        }
    }
}
using System;
using Math2D;
using UnityEditor;
using UnityEngine;

namespace Math2D
{
    public class PointGizmo : MonoBehaviour
    {
        public bool HideName;    
    
        //TODO Snap point to line if the point is contained on that line. Gotta rip the point out of need detachment
        protected virtual void OnDrawGizmos()
        {
            var text = HideName? "" : gameObject.name;

            if (Selection.activeTransform == transform)
            {
                text += HideName ? gameObject.name: "";
                
                var position = transform.position;
                text += " (" + Math.Round(position.x, 2) + ", " + Math.Round(position.y, 2) + ")";
            }

            Handles.Label(transform.position + new Vector3(-0.5f, 1f), text, EditorStyles.boldLabel);
        }
    }
}
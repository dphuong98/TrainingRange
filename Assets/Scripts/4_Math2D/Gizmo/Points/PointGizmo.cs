using System;
using Math2D;
using UnityEditor;
using UnityEngine;

namespace Math2D
{
    public class PointGizmo : MonoBehaviour
    {
        public bool HideName;
        public bool HideCoord;
    
        //TODO Snap point to line if the point is contained on that line. Gotta rip the point out of need detachment
        protected virtual void OnDrawGizmos()
        {
            var text = (HideName? "" : gameObject.name) + (HideCoord? "" : GetCoord());

            if (Selection.activeTransform == transform)
            {
                text += HideName ? gameObject.name: "";
                text += HideCoord ? GetCoord() : "";
            }
            
            Handles.Label(transform.position + new Vector3(-0.25f, 0.5f), text, EditorStyles.boldLabel);
        }

        private string GetCoord()
        {
            var position = transform.position;
            return " (" + Math.Round(position.x, 2) + ", " + Math.Round(position.y, 2) + ")";
        }
    }
}
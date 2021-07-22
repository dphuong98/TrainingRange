using System;
using UnityEditor;
using UnityEngine;

public class PointGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        var text = gameObject.name;

        if (Selection.activeTransform == transform)
        {
            var position = transform.position;
            text += " (" + Math.Round(position.x, 2) + ", " + Math.Round(position.y, 2) + ")";
        }
            
        Handles.Label(transform.position + new Vector3(-0.5f, 1f), text, EditorStyles.boldLabel);
    }
}

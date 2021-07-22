using System;
using System.Collections;
using System.Collections.Generic;
using Math2D;
using UnityEngine;

public class LineGizmo : MonoBehaviour
{
    public float a;
    public float b;
    public float c;
    public Color lineColor = Color.blue;
    
    private Line lineEquation;

    private void OnValidate()
    {
        if (gameObject.activeSelf == false) return; //OnValidate also update for prefabs
        
        try
        {
            SetEquation(new Line(a, b, c));
        }
        catch
        {
            // ignored
        }
    }

    private void OnDrawGizmos()
    {
        var point = lineEquation.Project(new Point(0, 0));
        var direction = new Vector3(-a, b, 0) * 20;
        
        Gizmos.color = lineColor;
        Gizmos.DrawRay(new Vector3(point.x, point.y, 0), direction);
        Gizmos.DrawRay(new Vector3(point.x, point.y, 0), -direction);
    }

    public void SetEquation(Line equation)
    {
        lineEquation = equation;
        name = lineEquation.ToString();
        a = lineEquation.a;
        b = lineEquation.b;
        c = lineEquation.c;
    }
}

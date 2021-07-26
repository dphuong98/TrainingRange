using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Math2D;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class IntersectionGizmo : MonoBehaviour
{
    public bool showIntersection;
    
    public Transform segments;
    public Transform lines;

    private void OnDrawGizmos()
    {
        if (!showIntersection) return;

        var intersection = new Point();
        
        for (var i = 0; i < segments.childCount; i++)
        {
            var s1 = GetSegment(i);
            
            //Segments x Segments
            for (var j = i + 1; j < segments.childCount; j++)
            {
                var s2 = GetSegment(j);

                if (s1.Intersect(s2, ref intersection))
                {
                    DrawIntersection(intersection);
                }
            }
            
            //Segments x Lines
            for (var j = 0; j < lines.childCount; j++)
            {
                var s2 = GetLine(j);
                
                if (s1.Intersect(s2, ref intersection))
                {
                    DrawIntersection(intersection);
                }
            }
        }
        
        //Lines x Lines
        for (var i = 0; i < lines.childCount; i++)
        {
            var s1 = GetLine(i);

            for (var j = i + 1; j < lines.childCount; j++)
            {
                var s2 = GetLine(j);
                
                if (s1.Intersect(s2, ref intersection))
                {
                    DrawIntersection(intersection);
                }
            }
        }
        
    }

    private void DrawIntersection(Point intersection)
    {
        var position = new Vector3(intersection.x, intersection.y, 0);
        var text = "(" + Math.Round(position.x, 2) + ", " + Math.Round(position.y, 2) + ")";
        Handles.Label(position + new Vector3(-0.25f, 0.25f), text);
    }

    private LineSegment GetSegment(int i)
    {
        return segments.GetChild(i).GetComponent<SegmentGizmo>().GetLineSegment();
    }
    
    private Line GetLine(int i)
    {
        return lines.GetChild(i).GetComponent<LineGizmo>().GetLine();
    }
}

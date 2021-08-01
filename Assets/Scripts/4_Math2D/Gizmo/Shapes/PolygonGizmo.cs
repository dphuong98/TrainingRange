using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Math2D;
using UnityEngine;

public class PolygonGizmo : MonoBehaviour
{
    [SerializeField]
    private Transform[] vertices;
    
    public Color polygonColor = Color.yellow;
    public Color brokenPolygonColor = Color.red;
    
    public Transform[] Vertices
    {
        get => vertices;
        set
        {
            if (value.Length < 3)
                return;
            vertices = value;

            DrawSegmentBetweenPoints();
        }
    }

    private void OnDrawGizmos()
    {
        if (vertices.Count(v => v == null) > 0)
        {
            DestroyProtocol();
            return;
        }
        
        var segments = SegmentManager.Instance;
        
        for (var i = 0; i < vertices.Length - 1; i++)
        {
            if (!segments.Exist(vertices[i], vertices[i+1]))
            {
                DestroyProtocol();
                return;
            }
        }

        if (!segments.Exist(vertices[vertices.Length - 1], vertices[0]))
        {
            DestroyProtocol();
            return;
        }
    }

    private void DrawSegmentBetweenPoints()
    {
        var segments = SegmentManager.Instance;
        
        for (var i = 0; i < vertices.Length - 1; i++)
        {
            if (!segments.Exist(vertices[i], vertices[i + 1]))
            {
                segments.SpawnSegment(vertices[i], vertices[i + 1]);
            }
            segments.GetSegment(vertices[i], vertices[i + 1]).GetComponent<SegmentGizmo>().segmentColor = polygonColor;
        }

        if (!segments.Exist(vertices[vertices.Length - 1], vertices[0]))
        {
            segments.SpawnSegment(vertices[vertices.Length - 1], vertices[0]);
        }
        segments.GetSegment(vertices[vertices.Length - 1], vertices[0]).GetComponent<SegmentGizmo>().segmentColor = polygonColor;
    }

    private void DestroyProtocol()
    {
        var segments = SegmentManager.Instance;
        
        for (var i = 0; i < vertices.Length - 1; i++)
        {
            if (vertices[i] != null && vertices[i + 1] != null && segments.Exist(vertices[i], vertices[i+1]))
            {
                segments.GetSegment(vertices[i], vertices[i + 1]).GetComponent<SegmentGizmo>().segmentColor = brokenPolygonColor;
            }
        }

        if (vertices[vertices.Length - 1] != null && vertices[0] != null && segments.Exist(vertices[vertices.Length - 1], vertices[0]))
        {
            segments.GetSegment(vertices[vertices.Length - 1], vertices[0]).GetComponent<SegmentGizmo>().segmentColor = brokenPolygonColor;
        }
        
        DestroyImmediate(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Math2D;
using UnityEditor;
using UnityEngine;

public class PolygonGizmo : MonoBehaviour, IShapeGizmo
{
    [SerializeField]
    protected Transform[] vertices;

    public Color polygonColor = Color.yellow;
    public Color brokenPolygonColor = Color.red;

    public void SetVertices(Transform[] vertices)
    {
        if (vertices.Length < 3)
            return;
        
        this.vertices = vertices;
        DrawSegmentBetweenPoints();
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
        
        var polygon = GetPolygon();
        if (Selection.activeTransform == transform)
        {
            var text = "Area: " + polygon.Area();
            Handles.Label(vertices[0].position + new Vector3(-0.5f, -0.5f), text, EditorStyles.boldLabel);
        }
    }

    public Polygon GetPolygon()
    {
        return new Polygon(vertices.Select(v => new Point(v.position.x, v.position.y)).ToArray());;
    }

    public List<Transform> GetSegments()
    {
        var segments = SegmentManager.Instance;
        var segmentList = vertices.Zip(vertices.Skip(1), (a, b) => segments.GetSegment(a, b)).ToList();
        segmentList.Add(segments.GetSegment(vertices.Last(), vertices.First()));
        return segmentList;
    }

    protected void DrawSegmentBetweenPoints()
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

public interface IShapeGizmo
{
    List<Transform> GetSegments();
}

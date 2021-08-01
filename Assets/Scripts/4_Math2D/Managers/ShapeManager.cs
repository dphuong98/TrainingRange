using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Math2D;
using UnityEngine;

public class ShapeManager : Singleton<ShapeManager>
{
    [SerializeField] private GameObject rectanglePrefab;
    [SerializeField] private GameObject polygonPrefab;

    public Transform SpawnPolygon(Transform[] vertices)
    {
        var polygonName = string.Join("", vertices.Select(v => v.name));
        if (Exist(polygonName)) return null;
        
        var newPolygon = Instantiate(polygonPrefab, transform);
        newPolygon.name = polygonName;

        var gizmo = newPolygon.GetComponent<PolygonGizmo>();
        gizmo.SetVertices(vertices);

        return newPolygon.transform;
    }

    public bool Exist(string name)
    {
        return transform.Find(name);
    }
}

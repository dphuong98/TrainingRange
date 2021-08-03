using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Math2D;
using UnityEditor;
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

    public Transform SpawnRectangle()
    {
        var width = 3f;
        var height = 2f;
        foreach (var c in "123456789")
        {
            var rectangleName = "R" + c;
            if (Exist(rectangleName)) continue;

            var newRectangle = Instantiate(rectanglePrefab, transform);
            newRectangle.name = rectangleName;
            
            var cameraPosition = SceneView.lastActiveSceneView.camera.transform.position;
            var topRight = new Vector3(cameraPosition.x + width / 2, cameraPosition.y + height / 2, 0);

            var gizmo = newRectangle.GetComponent<RectangleGizmo>();
            gizmo.SetVertices(topRight, width, height);
            
            return newRectangle.transform;
        }

        return null;
    }

    public bool Exist(string name)
    {
        return transform.Find(name);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Math2D
{
    public class PointManager : Singleton<PointManager>
    {
        [SerializeField] private GameObject pointPrefab;
        [SerializeField] private GameObject intersectionPrefab;
        [SerializeField] private GameObject projectionPrefab;

        public Transform SpawnPoint()
        {
            //TODO naming method after "Z"
            foreach (var c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
            {
                if (Exist(c.ToString()))
                    continue;
            
                var cameraPosition = SceneView.lastActiveSceneView.camera.transform.position;
                var pointPosition = new Vector3(cameraPosition.x, cameraPosition.y, 0);
                
                var newPoint = Instantiate(pointPrefab, pointPosition, Quaternion.identity, transform);
                newPoint.name = c.ToString();

                return newPoint.transform;
            }

            return null;
        }
    
        public Transform SpawnPoint(string name, Point position)
        {
            if (Exist(name)) return null;
        
            var newPoint = Instantiate(pointPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity, transform);
            newPoint.name = name;
            
            return newPoint.transform;
        }
    
        public Transform SpawnIntersection(Transform line1, Transform line2)
        {
            if (line1 == null || line2 == null) return null;

            var intersectionName = line1.name + " x " + line2.name;
            if (Exist(intersectionName)) return null;

            var newIntersection = Instantiate(intersectionPrefab, transform);
            newIntersection.name = intersectionName;

            var gizmo = newIntersection.GetComponent<IntersectionGizmo>();
            gizmo.line1 = line1;
            gizmo.line2 = line2;

            return newIntersection.transform;
        }

        public Transform SpawnProjection(Transform point, Transform line)
        {
            if (point == null || line == null) return null;
            
            var projectionName = point.name + " -> " + line.name;
            if (Exist(projectionName)) return null;
            
            var newProjection = Instantiate(projectionPrefab, transform);
            newProjection.name = projectionName;

            var gizmo = newProjection.GetComponent<ProjectionGizmo>();
            gizmo.point = point;
            gizmo.line = line;
            
            return newProjection.transform;
        }

        public Transform GetPoint(string name)
        {
            return transform.Find(name);
        }

        public bool Exist(string name)
        {
            return GetPoint(name) != null;
        }
    }
}
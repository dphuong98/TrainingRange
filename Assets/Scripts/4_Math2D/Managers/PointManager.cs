using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Math2D
{
    public class PointManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject pointPrefab;
        [SerializeField]
        private GameObject intersectionPrefab;

        public void SpawnPoint()
        {
            //TODO naming method after "Z"
            foreach (var c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
            {
                if (Exist(c.ToString()))
                    continue;
            
                var cameraPosition = SceneView.lastActiveSceneView.camera.transform.position;
                var newPoint = Instantiate(pointPrefab, cameraPosition, Quaternion.identity, transform);
                newPoint.name = c.ToString();
            
                //TODO remove hard code
                Selection.activeTransform = newPoint.transform;
                SceneView.lastActiveSceneView.FrameSelected();
                break;
            }
        }
    
        public void SpawnPoint(string name, Point position)
        {
            if (Exist(name)) return;
        
            Instantiate(pointPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity, transform).name = name;
        }
    
        public void SpawnIntersection(string name, Transform line1, Transform line2)
        {
            if (Exist(name)) return;
        
            var newIntersection = Instantiate(intersectionPrefab, transform);
            newIntersection.name = name;

            var gizmo = newIntersection.GetComponent<IntersectionGizmo>();
            gizmo.HideName = true;
            gizmo.line1 = line1;
            gizmo.line2 = line2;
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
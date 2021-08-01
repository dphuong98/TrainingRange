using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public class LineManager : Singleton<LineManager>
    {
        [SerializeField] private GameObject linePrefab;
    
        public Transform SpawnLine(Transform point1, Transform point2)
        {
            var newLineName = (point1.name + point2.name).Sort();
            if (Exist(newLineName)) return null;

            var newLine = Instantiate(linePrefab, transform);
            newLine.name = newLineName;

            var gizmo = newLine.GetComponent<LineGizmo>();
            gizmo.point1 = point1;
            gizmo.point2 = point2;

            return newLine.transform;
        }
        
        public Transform GetLine(string name)
        {
            return transform.Find(name);
        }

        public bool Exist(string name)
        {
            return GetLine(name) != null;
        }
    }
}
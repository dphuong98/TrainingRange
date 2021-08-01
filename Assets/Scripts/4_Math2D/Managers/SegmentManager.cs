using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public class SegmentManager : Singleton<SegmentManager>
    {
        [SerializeField] private GameObject segmentPrefab;
    
        public Transform SpawnSegment(Transform point1, Transform point2)
        {
            //TODO Projection line naming
            var newSegmentName = point1.name.CompareTo(point2.name) < 0 ? point1.name + point2.name : point2.name + point1.name;
            if (Exist(newSegmentName)) return null;
            
            var newSegment = Instantiate(segmentPrefab, transform);
            newSegment.name = newSegmentName;
            
            var gizmo = newSegment.GetComponent<SegmentGizmo>();
            gizmo.point1 = point1;
            gizmo.point2 = point2;

            return newSegment.transform;
        }
        
        public Transform GetSegment(string name)
        {
            return transform.Find(name);
        }

        public Transform GetSegment(Transform point1, Transform point2)
        {
            var segmentName = point1.name.CompareTo(point2.name) < 0 ? point1.name + point2.name : point2.name + point1.name;
            return GetSegment(segmentName);
        }

        public bool Exist(string name)
        {
            return GetSegment(name) != null;
        }

        public bool Exist(Transform point1, Transform point2)
        {
            var segmentName = point1.name.CompareTo(point2.name) < 0 ? point1.name + point2.name : point2.name + point1.name;
            return Exist(segmentName);
        }
    }
}
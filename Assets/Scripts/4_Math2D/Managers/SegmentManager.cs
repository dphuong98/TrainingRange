using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public class SegmentManager : MonoBehaviour
    {
        [SerializeField] private GameObject segmentPrefab;
    
        public GameObject SpawnSegment(GameObject point1, GameObject point2)
        {
            //TODO Projection line naming
            var newSegmentName = point1.name.CompareTo(point2.name) < 0 ? point1.name + point2.name : point2.name + point1.name;
            if (Exist(newSegmentName)) return null;
            
            var newSegment = Instantiate(segmentPrefab, transform);
            newSegment.name = newSegmentName;
            
            var gizmo = newSegment.GetComponent<SegmentGizmo>();
            gizmo.point1 = point1;
            gizmo.point2 = point2;

            return newSegment;
        }
        
        public Transform GetSegment(string name)
        {
            return transform.Find(name);
        }

        public bool Exist(string name)
        {
            return GetSegment(name) != null;
        }
    }
}
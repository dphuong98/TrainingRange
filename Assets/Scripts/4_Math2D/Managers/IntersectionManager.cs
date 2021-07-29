using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Math2D
{
    public class IntersectionManager : MonoBehaviour
    {
        public static bool ShowIntersection = true;
        public static bool ShowCoordinate;

        public Transform segments;
        public Transform lines;
    
        [SerializeField]
        private PointManager Points;

        private void OnDrawGizmos()
        {
            if (!ShowIntersection) return;
            
            var intersection = new Point();

            var lineGroup = segments.Cast<Transform>().Concat(lines.Cast<Transform>()).ToArray();

            for (var i = 0; i < lineGroup.Count(); i++)
            {
                var line1 = lineGroup[i].GetLine();
                for (var j = i + 1; j < lineGroup.Count(); j++)
                {
                    var line2 = lineGroup[j].GetLine();
                
                    if (!line1.Intersect(line2, ref intersection)) continue;
                    
                    Points.SpawnIntersection(lineGroup[i], lineGroup[j]);
                }
            }
        }
    }
}
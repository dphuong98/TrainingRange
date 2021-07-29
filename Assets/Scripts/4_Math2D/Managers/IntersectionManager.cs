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
        public bool showIntersection;

        public Transform segments;
        public Transform lines;
    
        [SerializeField]
        private PointManager Points;

        private void OnDrawGizmos()
        {
            if (!showIntersection) return;

            var intersection = new Point();

            var lineGroup = segments.Cast<Transform>().Concat(lines.Cast<Transform>()).ToArray();

            for (var i = 0; i < lineGroup.Count(); i++)
            {
                var line1 = lineGroup[i].GetLine();
                for (var j = i + 1; j < lineGroup.Count(); j++)
                {
                    var line2 = lineGroup[j].GetLine();
                
                    if (!line1.Intersect(line2, ref intersection)) continue;

                    var intersectionName = lineGroup[i].name + " x " + lineGroup[j].name;
                    if (!Points.Exist(intersectionName))
                    {
                        Points.SpawnIntersection(intersectionName, lineGroup[i], lineGroup[j]);
                    }
                }
            }
        }
    }
}
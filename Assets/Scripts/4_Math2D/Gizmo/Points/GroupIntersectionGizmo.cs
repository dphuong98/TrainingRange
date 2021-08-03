using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Math2D
{
    public class GroupIntersectionGizmo : ConditionalPointGizmo
    {
        public Transform line;
        public Transform shape;

        protected override bool IsValid()
        {
            return line != null && shape != null;
        }

        private void OnDrawGizmos()
        {
            var points = PointManager.Instance;

            var shapeSegments = shape.GetShapeGizmo().GetSegments();
            foreach (var segment in shapeSegments)
            {
                var intersection = new Point();
                if (line.GetLine().Intersect(segment.GetLine(), ref intersection))
                    points.SpawnIntersection(line, segment);
            }
        }
    }
}
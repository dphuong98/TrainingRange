using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public class IntersectionGizmo: ConditionalPointGizmo
    {
        public Transform line1;
        public Transform line2;
    
        protected override bool IsValid()
        {
            if (line1 == null || line2 == null)
                return false;
            
            if (!IntersectionManager.ShowIntersection) return false;
            
            var intersectionPoint = new Point();
            var equation1 = line1.GetLine();
            var equation2 = line2.GetLine();
            if (!equation1.Intersect(equation2, ref intersectionPoint)) return false;
            
            HideCoord = !IntersectionManager.ShowCoordinate;

            transform.position = new Vector3(intersectionPoint.x, intersectionPoint.y, 0);
            return true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public class ProjectionGizmo : ConditionalPointGizmo
    {
        public Transform point;
        public Transform line;
        
        protected override bool IsValid()
        {
            if (point == null || line == null)
                return false;

            var lineEquation = line.GetLine();
            var projectionPoint = lineEquation.Project(new Point(point.position.x, point.position.y));
            
            transform.position = new Vector3(projectionPoint.x, projectionPoint.y, 0);
            return true;
        }
    }
}
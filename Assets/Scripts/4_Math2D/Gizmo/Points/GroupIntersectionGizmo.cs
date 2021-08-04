using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Math2D
{
    public class GroupIntersectionGizmo : MonoBehaviour
    {
        public Transform line;
        public Transform shape;

        [SerializeField]
        private Transform circleIntersection1;
        [SerializeField]
        private Transform circleIntersection2;

        private void OnDrawGizmos()
        {
            if (line == null || shape == null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            
            var points = PointManager.Instance;

            var shapeGizmo = shape.GetShapeGizmo();
            if (shapeGizmo is PolygonGizmo gizmo)
            {
                var shapeSegments = gizmo.GetSegments();
                foreach (var segment in shapeSegments)
                {
                    var intersection = new Point();
                    if (line.GetLine().Intersect(segment.GetLine(), ref intersection))
                        points.SpawnIntersection(line, segment);
                }
            }

            if (shapeGizmo is CircleGizmo circleGizmo)
            {
                //TODO remove hardcode
                var circle = circleGizmo.GetCircle();
                var intersections = new List<Point>();
                circle.Intersect(line.GetLine(), ref intersections);
                
                var intersectionName1 = name + "-0";
                var intersectionName2 = name + "-1";
                switch (intersections.Count)
                {
                    case 0:
                        if (circleIntersection1 != null)
                            DestroyImmediate(circleIntersection1.gameObject);
                        if (circleIntersection2 != null)
                            DestroyImmediate(circleIntersection2.gameObject);
                        break;
                    case 1:
                        if (circleIntersection1 != null)
                            circleIntersection1.position = new Vector3(intersections[0].x, intersections[0].y, 0);
                        else
                            circleIntersection1 = points.SpawnPoint(intersectionName1, new Point(intersections[0].x, intersections[0].y));
                        if (circleIntersection2 != null)
                            DestroyImmediate(circleIntersection2.gameObject);
                        break;
                    case 2:
                        if (circleIntersection1 != null)
                            circleIntersection1.position = new Vector3(intersections[0].x, intersections[0].y, 0);
                        else
                            circleIntersection1 = points.SpawnPoint(intersectionName1, new Point(intersections[0].x, intersections[0].y));
                        if (circleIntersection2 != null)
                            circleIntersection2.position = new Vector3(intersections[1].x, intersections[1].y, 0);
                        else
                            circleIntersection2 = points.SpawnPoint(intersectionName2, new Point(intersections[1].x, intersections[1].y));
                        break;
                }
            }
        }
    }
}
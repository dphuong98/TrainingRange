using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Math2D
{
    public static class TransformExtension
    {
        public static ILine GetLine(this Transform transform)
        {
            var segment = transform.GetComponent<SegmentGizmo>();
            if (segment != null)
            {
                return segment.GetLineSegment();
            }

            var line = transform.GetComponent<LineGizmo>();
            if (line != null)
            {
                return line.GetLine();
            }

            return null;
        }
        
        public static IShape GetShape(this Transform transform)
        {
            var polygon = transform.GetComponent<PolygonGizmo>();
            if (polygon != null)
            {
                return polygon.GetPolygon();
            }
            
            var rectangle = transform.GetComponent<RectangleGizmo>();
            if (rectangle != null)
            {
                //return rectangle.GetLineSegment();
            }

            return null;
        }

        public static bool IsPoint(this Transform transform)
        {
            return transform.GetComponent<PointGizmo>() != null || transform.GetComponent<IntersectionGizmo>() != null || transform.GetComponent<ProjectionGizmo>() != null;
        }
        
        public static bool IsLine(this Transform transform)
        {
            return transform.GetComponent<SegmentGizmo>() != null || transform.GetComponent<LineGizmo>() != null;
        }

        public static bool IsShape(this Transform transform)
        {
            return transform.GetComponent<PolygonGizmo>() != null || transform.GetComponent<RectangleGizmo>() != null;
        }
    }
}
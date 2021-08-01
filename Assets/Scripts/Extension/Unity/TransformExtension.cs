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

        public static bool IsSegment(this Transform segmentTransform)
        {
            return segmentTransform.GetComponent<SegmentGizmo>() != null;
        }
        
        public static bool IsLine(this Transform lineTransform)
        {
            return lineTransform.GetComponent<LineGizmo>() != null;
        }
        
        public static bool IsPoint(this Transform pointTransform)
        {
            return pointTransform.GetComponent<PointGizmo>() != null;
        }
    }
}
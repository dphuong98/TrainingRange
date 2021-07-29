using System.Collections;
using System.Collections.Generic;
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
    }
}
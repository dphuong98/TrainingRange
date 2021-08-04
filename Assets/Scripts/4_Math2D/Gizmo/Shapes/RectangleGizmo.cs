using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Math2D
{
    public class RectangleGizmo : PolygonGizmo
    {
        private Point topRight;
        private float width, height;
    
        //TODO Invalidate rectangle when rectangle conditions become untrue
    
        public void SetVertices(Vector3 _topRight, float _width, float _height)
        {
            topRight = new Point(_topRight.x, _topRight.y);
            width = _width;
            height = _height;
        
            var points = PointManager.Instance;
            var verticesPoint = new Point[]
            {
                topRight,
                new Point(topRight.x, topRight.y - height),
                new Point(topRight.x - width, topRight.y - height),
                new Point(topRight.x - width, topRight.y),
            };
        
            vertices = verticesPoint.Select((v, i) => points.SpawnPoint(name + "-" + i, v)).ToArray();
            DrawSegmentBetweenPoints();
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(topRight, width, height);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math2D
{
    public abstract class ConditionalPointGizmo : PointGizmo
    {
        protected abstract bool IsValid();

        protected override void OnDrawGizmos()
        {
            //TODO Error warning in editor
            if (!IsValid())
            {
                if (gameObject.IsPrefab()) return;
            
                DestroyImmediate(gameObject);
                return;
            }
            
            base.OnDrawGizmos();
        }
    }
}
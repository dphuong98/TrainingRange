using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    public static bool IsPrefab(this GameObject gameObject)
    {
        return string.IsNullOrEmpty(gameObject.scene.path)
               && !string.IsNullOrEmpty(gameObject.scene.name);
    }
}

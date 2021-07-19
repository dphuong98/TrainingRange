using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Math2DWindow : EditorWindow
{

    [MenuItem("Custom/Math2D")]
    private static void ShowWindow()
    {
        GetWindow<Math2DWindow>("Math2D");
    }

    private void OnGUI()
    {
        GUILayout.Label("Here I am");
    }
}

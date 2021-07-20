using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class Math2DWindow : EditorWindow
{
    private float buttonWidth = 50f;
    private float buttonHeight = 50f;
    
    [MenuItem("Custom/Math2D")]
    private static void ShowWindow()
    {
        GetWindow<Math2DWindow>("Math2D");
    }
    
    // private void OnGUI()
    // {
    //     GUILayout.Label("Drag & drop a point onto the scene");
    //
    //     GUILayout.BeginHorizontal();
    //     if (GUILayout.Button(Resources.Load<Texture>("Icons/Point"), GUILayout.Width(buttonWidth),
    //         GUILayout.Height(buttonHeight)))
    //     {
    //         
    //     }
    //     GUILayout.EndHorizontal();
    // }
    
    private GameObject prefab;
    private void OnEnable()
    {
        var label = new Label("Here at the gate");

        prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Cube.prefab");
 
        var box = new VisualElement();
        box.style.flexGrow = 1f;
 
        box.RegisterCallback<MouseDownEvent>(evt =>
        {
            DragAndDrop.PrepareStartDrag();
            DragAndDrop.StartDrag("Dragging");
            DragAndDrop.objectReferences = new Object[] { prefab };
        });
 
        box.RegisterCallback<DragUpdatedEvent>(evt =>
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Move;
        });
 
        rootVisualElement.Add(box);
    }
}

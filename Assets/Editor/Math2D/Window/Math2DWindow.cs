using System;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Math2D;
using TreeEditor;
using UnityEngine.UIElements;

public class Math2DWindow : EditorWindow
{
    private bool isFloating;
    private float windowMinWidth = 200;
    private float windowMinHeight = 270;
    private float buttonWidth = 50f;
    private float buttonHeight = 50f;

    private GameObject math2D;
    
    private PointManager points;
    private SegmentManager segments;
    private LineManager lines;

    [MenuItem("Custom/Math2D")]
    private static void ShowWindow()
    {
        GetWindow<Math2DWindow>("Math2D");
    }
    
    private void Awake()
    {
        minSize = new Vector2(windowMinWidth, windowMinHeight);
        
        math2D = GameObject.Find("Math2D");
        if (math2D == null)
        {
            math2D = (GameObject) Instantiate(Resources.Load("Prefabs/Math2D/Math2D"));
            math2D.name = "Math2D";
        }
        
        points = math2D.transform.Find("Points").GetComponent<PointManager>();
        segments = math2D.transform.Find("Segments").GetComponent<SegmentManager>();
        lines = math2D.transform.Find("Lines").GetComponent<LineManager>();
    }

    private void SetIsUtilityWindow(bool isUtilityWindow)
    {
        var windowTitle = titleContent;
        Close();
        var res = GetWindow(GetType(), isUtilityWindow);
        ((Math2DWindow) res).isFloating = isUtilityWindow;
        res.titleContent = windowTitle;
    }

    private void ShowGenericMenu()
    {
        var menu = new GenericMenu();
        menu.AddItem(new GUIContent("Window/Open as Floating Window", ""), isFloating, () => SetIsUtilityWindow(true));
        menu.AddItem(new GUIContent("Window/Open as Dockable Window", ""), !isFloating, () => SetIsUtilityWindow(false));
        menu.ShowAsContext();
    }

    private void OnGUI()
    {
        var e = Event.current;
        switch (e.type)
        {
            case EventType.ContextClick:
                ShowGenericMenu();
                break;
        }
        
        GUILayout.BeginHorizontal();
        GUILayout.Space(5f);
        GUILayout.BeginVertical();
        if (GUILayout.Button(Resources.Load<Texture>("Icons/Point"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            //TODO spawn folder if not found
            points.SpawnPoint();
        }

        if (GUILayout.Button(Resources.Load<Texture>("Icons/Segment"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            var points = getSelectedGameObjects(2);
            if (points != null)
                segments.SpawnSegment(points[0], points[1]);
        }

        if (GUILayout.Button(Resources.Load<Texture>("Icons/Line"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            var points = getSelectedGameObjects(2);
            if (points != null)
                lines.SpawnLine(points[0], points[1]);
        }
        
        if (GUILayout.Button(Resources.Load<Texture>("Icons/Intersect"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            var lines = getSelectedGameObjects(2);
            if (lines != null)
            {
                var line1 = lines[0].transform.GetLine();
                var line2 = lines[1].transform.GetLine();
                
                if (line1 != null && line2 != null)
                    points.SpawnIntersection(lines[0].transform, lines[1].transform);
            }
        }
        
        if (GUILayout.Button(Resources.Load<Texture>("Icons/Projection"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            var elements = getSelectedGameObjects(2);
            if (elements != null)
            {
                //TODO Refactor
                var element1 = elements[0].transform.GetLine();
                var element2 = elements[1].transform.GetLine();

                if (element1 == null && element2 != null)
                {
                    //Element 1 is a point and element 2 is a line/segment
                    var projection = points.SpawnProjection(elements[0].transform, elements[1].transform);
                    segments.SpawnSegment(projection, elements[0]);
                }
                
                if (element2 == null && element1 != null)
                {
                    //Element 2 is a point and element 1 is a line/segment
                    var projection = points.SpawnProjection(elements[1].transform, elements[0].transform);
                    segments.SpawnSegment(projection, elements[1]);
                }
            }
        }

        GUILayout.EndVertical();
        
        GUILayout.BeginVertical();
        GUILayout.Label("Intersections", EditorStyles.boldLabel);
        IntersectionGizmo.ShowCoordinate = GUILayout.Toggle(IntersectionGizmo.ShowCoordinate,"ShowCoordinate");
        GUILayout.EndVertical();
        
        GUILayout.EndHorizontal();
    }

    private GameObject[] getSelectedGameObjects(int count)
    {
        if (Selection.objects.Length != count) return null;
        return Selection.objects.Take(count).Select(s => (GameObject)s).ToArray();
    }
}

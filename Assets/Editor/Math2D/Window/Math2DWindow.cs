using System;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Math2D;
using TreeEditor;
using UnityEngine.UIElements;
using PointerType = UnityEngine.PointerType;

public class Math2DWindow : EditorWindow
{
    private bool isFloating;
    private float windowMinWidth = 130;
    private float windowMinHeight = 320;
    private float buttonWidth = 50f;
    private float buttonHeight = 50f;

    private GameObject math2D;
    
    private PointManager points;
    private SegmentManager segments;
    private LineManager lines;
    private ShapeManager shapes;

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
        
        //TODO spawn folder gameobject if not found
        points = PointManager.Instance;
        segments = SegmentManager.Instance;
        lines = LineManager.Instance;
        shapes = ShapeManager.Instance;
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
            points.SpawnPoint();
        }

        if (GUILayout.Button(Resources.Load<Texture>("Icons/Segment"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            var points = getSelectedGameObjects(2);
            if (points != null && points[0].IsPoint() && points[1].IsPoint())
                segments.SpawnSegment(points[0], points[1]);
        }

        if (GUILayout.Button(Resources.Load<Texture>("Icons/Line"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            var points = getSelectedGameObjects(2);
            if (points != null && points[0].IsPoint() && points[1].IsPoint())
                lines.SpawnLine(points[0], points[1]);
        }
        
        if (GUILayout.Button(Resources.Load<Texture>("Icons/Projection"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            //TODO Probably refactor these
            var elements = getSelectedGameObjects(2);
            if (elements != null)
            {
                if (elements[0].IsPoint() && elements[1].IsLine())
                {
                    var projection = points.SpawnProjection(elements[0], elements[1]);
                    segments.SpawnSegment(projection, elements[0]);
                }
                
                if (elements[0].IsLine() && elements[1].IsPoint())
                {
                    var projection = points.SpawnProjection(elements[1], elements[0]);
                    segments.SpawnSegment(projection, elements[1]);
                }
            }
        }
        
        if (GUILayout.Button(Resources.Load<Texture>("Icons/Intersect"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            var elements = getSelectedGameObjects(2);
            if (elements != null)
            {
                if (elements[0].IsLine() && elements[1].IsLine())
                    points.SpawnIntersection(elements[0], elements[1]);

                if (elements[0].IsLine() && elements[1].IsShape())
                {
                    points.SpawnGroupIntersection(elements[0], elements[1]);
                }
                
                if (elements[0].IsShape() && elements[1].IsLine())
                {
                    points.SpawnGroupIntersection(elements[1], elements[0]);
                }
            }
        }
        GUILayout.EndVertical();
        
        GUILayout.BeginVertical();
        if (GUILayout.Button(Resources.Load<Texture>("Icons/Polygon"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            var vertices = getMinSelectedGameObjects(3);
            if (vertices != null)
            {
                shapes.SpawnPolygon(vertices);
            }
        }
        
        if (GUILayout.Button(Resources.Load<Texture>("Icons/Contain"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            var elements = getSelectedGameObjects(2);
            if (elements != null)
            {
                if (elements[0].IsPoint() && elements[1].IsShape())
                {
                    points.AddPointProximityChecker(elements[0], elements[1]);
                }
                
                if (elements[0].IsShape() && elements[1].IsPoint())
                {
                    points.AddPointProximityChecker(elements[1], elements[0]);
                }
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        GUILayout.BeginVertical();

        GUILayout.BeginVertical();
        GUILayout.Label("Intersections", EditorStyles.boldLabel);
        IntersectionGizmo.ShowCoordinate = GUILayout.Toggle(IntersectionGizmo.ShowCoordinate,"ShowCoordinate");
        GUILayout.EndVertical();
        
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
    
    private Transform[] getSelectedGameObjects(int count)
    {
        if (Selection.objects.Length != count) return null;
        return Selection.objects.Take(count).Select(s => ((GameObject)s).transform).ToArray();
    }

    private Transform[] getMinSelectedGameObjects(int min)
    {
        if (Selection.objects.Length < min) return null;
        return Selection.objects.Select(s => ((GameObject)s).transform).ToArray();;
    }
}

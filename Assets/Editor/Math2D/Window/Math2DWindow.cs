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
    
    void OnInspectorUpdate()
    {
        Repaint();
    }

    private void OnGUI()
    {
        //TODO Window only update when hovered over
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

        {
            GUI.enabled = true;
            var buttonContent =
                new GUIContent(Resources.Load<Texture>("Icons/Point"), "Spawn a new point at the center of scene view");
            if (GUILayout.Button(buttonContent, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
                    points.SpawnPoint();
        }

        {
            var elements = getSelectedGameObjects(2);
            GUI.enabled = elements != null && elements[0].IsPoint() && elements[1].IsPoint();
            var segmentContent =
                new GUIContent(Resources.Load<Texture>("Icons/Segment"), "Create a segment between 2 points selected in scene view");
            if (GUILayout.Button(segmentContent, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
                    segments.SpawnSegment(elements[0], elements[1]);
            }

        {
            var elements = getSelectedGameObjects(2);
            GUI.enabled = elements != null && elements[0].IsPoint() && elements[1].IsPoint();
            var lineContent =
                new GUIContent(Resources.Load<Texture>("Icons/Line"), "Create a line between 2 points selected in scene view");
            if (GUILayout.Button(lineContent, GUILayout.Width(buttonWidth),
                GUILayout.Height(buttonHeight)))
                    lines.SpawnLine(elements[0], elements[1]);
        }

        {
            var elements = getSelectedGameObjects(2);
            GUI.enabled = elements != null
                          && ((elements[0].IsPoint() && elements[1].IsLine()) 
                              || (elements[0].IsLine() && elements[1].IsPoint()));
            var projectionContent =
                new GUIContent(Resources.Load<Texture>("Icons/Projection"), "Create a projection between a point and a line/segment selected in scene view");
            if (GUILayout.Button(projectionContent, GUILayout.Width(buttonWidth),
                GUILayout.Height(buttonHeight)))
            {
                //TODO Probably refactor these
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

        {
            var elements = getSelectedGameObjects(2);
            GUI.enabled = elements != null
                          && ((elements[0].IsLine() && elements[1].IsLine())
                              || (elements[0].IsLine() && elements[1].IsShape())
                              || (elements[0].IsShape() && elements[1].IsLine()));
            var intersectContent =
                new GUIContent(Resources.Load<Texture>("Icons/Intersect"), "Create an intersection between 2 lines or a line and a shape selected in scene view");
            if (GUILayout.Button(intersectContent, GUILayout.Width(buttonWidth),
                GUILayout.Height(buttonHeight)))
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
        {
            var vertices = getMinSelectedGameObjects(3);
            GUI.enabled = vertices != null;
            var polygonContent =
                new GUIContent(Resources.Load<Texture>("Icons/Polygon"), "Define a polygon from selected points in scene view");
            if (GUILayout.Button(polygonContent, GUILayout.Width(buttonWidth),
                GUILayout.Height(buttonHeight)))
                    shapes.SpawnPolygon(vertices);
        }

        {
            var elements = getSelectedGameObjects(2);
            GUI.enabled = elements != null
                && ((elements[0].IsPoint() && elements[1].IsShape())
                    ||(elements[0].IsShape() && elements[1].IsPoint()));
            var containContent =
                new GUIContent(Resources.Load<Texture>("Icons/Contain"), "Make a point aware of its position relative to a shape");
            if (GUILayout.Button(containContent, GUILayout.Width(buttonWidth),
                GUILayout.Height(buttonHeight)))
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
        GUI.enabled = true;
        GUILayout.Label("Intersections", EditorStyles.boldLabel);
        IntersectionGizmo.ShowCoordinate = GUILayout.Toggle(IntersectionGizmo.ShowCoordinate, new GUIContent("ShowCoordinate", "Show the coordinate of intersection points without needing to selecting them in scene view"));
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

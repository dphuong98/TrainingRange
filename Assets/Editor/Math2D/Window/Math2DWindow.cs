using UnityEditor;
using UnityEngine;
using System.Linq;
using Math2D;
using TreeEditor;

public class Math2DWindow : EditorWindow
{
    private float buttonWidth = 50f;
    private float buttonHeight = 50f;

    private GameObject math2D;
    private Transform points;
    private Transform segments;
    private Transform lines;
    
    private GameObject pointPrefab;
    private GameObject segmentPrefab;
    private GameObject linePrefab;

    [MenuItem("Custom/Math2D")]
    private static void ShowWindow()
    {
        GetWindow<Math2DWindow>("Math2D");
    }

    private void Awake()
    {
        math2D = GameObject.Find("Math2D");
        if (math2D == null)
        {
            math2D = (GameObject) Instantiate(Resources.Load("Prefabs/Math2D/Math2D"));
            math2D.name = "Math2D";
        }

        points = math2D.transform.Find("Points");
        segments = math2D.transform.Find("Segments");
        lines = math2D.transform.Find("Lines");
        
        pointPrefab = Resources.Load("Prefabs/Math2D/Point") as GameObject;
        segmentPrefab = Resources.Load("Prefabs/Math2D/Segment") as GameObject;
        linePrefab = Resources.Load("Prefabs/Math2D/Line") as GameObject;
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(Resources.Load<Texture>("Icons/Point"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            SpawnPoint();
        }
        
        if (GUILayout.Button(Resources.Load<Texture>("Icons/Segment"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            SpawnSegment();
        }
        
        if (GUILayout.Button(Resources.Load<Texture>("Icons/Line"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            SpawnLine();
        }
        GUILayout.EndHorizontal();
    }

    private void SpawnLine()
    {
        Line newLineEquation;
        
        if (Selection.objects.Length == 2)
        {
            //Line through 2 points
            var points = Selection.objects.Take(2).Select(s => ((GameObject)s).transform.position).ToArray();
            newLineEquation = new Line(new Point(points[0].x, points[0].y), new Point(points[1].x, points[1].y));
        }
        else
        {
            //New line: y = x
            newLineEquation = new Line(-1, 1, 0);
        }

        if (lines.Find(newLineEquation.ToString()) != null) return;
        
        Instantiate(linePrefab, lines).GetComponent<LineGizmo>().SetEquation(newLineEquation);
    }

    private void SpawnPoint()
    {
        //TODO naming method after "Z"
        foreach (var c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            if (points.Find(c.ToString()) != null)
                continue;
            
            var cameraPosition = SceneView.lastActiveSceneView.camera.transform.position;
            var newPoint = Instantiate(pointPrefab, cameraPosition, Quaternion.identity, points);
            newPoint.name = c.ToString();
            
            Selection.activeTransform = newPoint.transform;
            SceneView.lastActiveSceneView.FrameSelected();
            break;
        }
    }

    void SpawnSegment()
    {
        if (Selection.objects.Length == 2)
        {
            var points = Selection.objects.Take(2).Select(s => (GameObject)s).ToArray();
            
            var newSegmentName =  (points[0].name + points[1].name).Sort();
            if (segments.Find(newSegmentName) != null) return;
            
            var newSegment = Instantiate(segmentPrefab, segments);
            newSegment.name = newSegmentName;
            
            var gizmo = newSegment.GetComponent<SegmentGizmo>();
            gizmo.point1 = points[0];
            gizmo.point2 = points[1];
        }
    }
}

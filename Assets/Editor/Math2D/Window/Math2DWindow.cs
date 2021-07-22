using UnityEditor;
using UnityEngine;
using System.Linq;
public class Math2DWindow : EditorWindow
{
    private float buttonWidth = 50f;
    private float buttonHeight = 50f;

    private GameObject math2D;
    private GameObject pointPrefab;

    private Transform points;

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
        
        pointPrefab = Resources.Load("Prefabs/Math2D/Point") as GameObject;
        Debug.Log(pointPrefab);
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(Resources.Load<Texture>("Icons/Point"), GUILayout.Width(buttonWidth),
            GUILayout.Height(buttonHeight)))
        {
            SpawnPoint();
        }
        GUILayout.EndHorizontal();
    }

    private void SpawnPoint()
    {
        var cameraPosition = SceneView.lastActiveSceneView.camera.transform.position;
        var newPoint = Instantiate(pointPrefab, cameraPosition, Quaternion.identity, points);
        foreach (var c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            if (points.Find(c.ToString()) != null)
                continue;

            newPoint.name = c.ToString();
            break;
            //TODO naming method after "Z"
        }

        Selection.activeTransform = newPoint.transform;
        SceneView.lastActiveSceneView.FrameSelected();
    }
}

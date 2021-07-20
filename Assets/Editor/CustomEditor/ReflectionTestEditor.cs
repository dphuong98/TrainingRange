using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ReflectionTest))]
public class ReflectionTestEditor : Editor
{
    private BindingFlags options =     BindingFlags.Instance |
                                       BindingFlags.NonPublic |
                                       BindingFlags.Public |
                                       BindingFlags.Static;

    private int toolbarInt;
    private readonly string[] toolbarStrings = {"Fields", "Properties", "Methods"};
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20f);
        var reflectTarget = ((ReflectionTest) target).ReflectionTarget;
        var reflectedType = reflectTarget.GetType();

        // DeclaredOnly
        if (((ReflectionTest) target).DeclaredOnly)
            options |= BindingFlags.DeclaredOnly;
        else
            options &= ~BindingFlags.DeclaredOnly;
        
        // Info
        GUILayout.Label("Type: " + reflectedType.FullName, EditorStyles.boldLabel);
        
        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
        switch (toolbarInt)
        {
            case 0: // Fields
                reflectedType.GetFields(options)
                    .Select(f => f.Modifier() + " " + f.FieldType.Name + " " + f.Name)
                    .ForEach(f => GUILayout.Label(f));
                break;
            case 1: // Properties
                reflectedType.GetProperties(options)
                    .Select(p => p.Modifier() + " " + p.PropertyType.Name + " " + p.Name + " { " + String.JoinFilter(", ",  p.CanRead ? p.GetMethod.Modifier() + " " + p.GetMethod: "", p.CanWrite ? p.SetMethod.Modifier() + " " + p.SetMethod: "") +" }")
                    .ForEach(f => GUILayout.Label(f));
                break;
            case 2: // Methods
                reflectedType.GetMethods(options)
                    .Select(m => m.Modifier() +" "+ m.ReturnType.Name +" "+ m.Name +" (" + string.Join(", ", m.GetParameters().Select(p => p.ToString()).ToArray()) + ") ")
                    .ForEach(f => GUILayout.Label(f));
                break;
        }
    }
}
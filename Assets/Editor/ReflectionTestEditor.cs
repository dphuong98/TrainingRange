using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ReflectionTest))]
public class ReflectionTestEditor : Editor
{
    private BindingFlags options =     BindingFlags.Instance |
                                       BindingFlags.NonPublic |
                                       BindingFlags.Public |
                                       BindingFlags.Static;

    private int toolbarInt = 0;
    private string[] toolbarStrings = {"Fields", "Properties", "Methods"};
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20f);
        var reflectTarget = ((ReflectionTest) target).ReflectionTarget;
        var reflectedType = reflectTarget.GetType();
        //TODO Beside Monoscript, are there any class that force encapsulate the class we are trying to target?
        if (reflectedType == typeof(MonoScript))
        {
            reflectedType = ((MonoScript) reflectTarget).GetClass();
        }

        //FlattenHierarchy update
        if (((ReflectionTest) target).DeclaredOnly)
            options |= (BindingFlags.DeclaredOnly);
        else
            options &= ~BindingFlags.DeclaredOnly;
        
        //Info
        GUILayout.Label("Type: " + reflectedType.FullName, EditorStyles.boldLabel);
        
        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
        switch (toolbarInt)
        {
            case 0: // Fields
                var reflectedFields = reflectedType.GetFields(options).Select(f => f.AccessModifier().ToString().ToLower() +" "+ f.FieldType.Name +" "+ f.Name);
                foreach (var f in reflectedFields)
                    GUILayout.Label(f);
                break;
            case 1: //Properties
                var reflectedProperties = reflectedType.GetProperties(options);
                foreach (var p in reflectedProperties)
                {
                    var propertySignature = p.AccessModifier().ToString().ToLower() +" "+ p.PropertyType.Name +" "+ p.Name + " { ";
                    propertySignature += (p.CanRead? p.GetMethod.AccessModifier().ToString().ToLower(): "") +" "+ p.GetMethod;
                    propertySignature += p.CanRead && p.CanWrite ? ", " : "";
                    propertySignature += (p.CanWrite? p.SetMethod.AccessModifier().ToString().ToLower(): "") +" "+ p.SetMethod;
                    propertySignature += " }";
                    GUILayout.Label(propertySignature);
                }
                break;
            case 2: //Methods
                var reflectedMethods = reflectedType.GetMethods(options);
                foreach (var m in reflectedMethods)
                {
                    var methodSignature = m.AccessModifier().ToString().ToLower() +" "+ m.ReturnType.Name +" "+ m.Name +" (";
                    
                    var arguments = m.GetParameters();
                    for (int i = 0; i < arguments.Length; i++)
                    {
                        methodSignature += arguments[i];
                        if (i != arguments.Length - 1) methodSignature += ", ";
                    }
                
                    methodSignature += ")";
                    GUILayout.Label(methodSignature);
                }
                break;
        }
    }
}
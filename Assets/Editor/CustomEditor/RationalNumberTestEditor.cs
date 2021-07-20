using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RationalNumberTest))]
public class RationalNumberTestEditor : Editor
{
    GUILayoutOption[] layoutOptions = { GUILayout.MaxWidth(50f) };

    int a_num, a_den; //Numerator and denominator of first rational number operand
    int b_num, b_den; //... second rational number operand
    string res_num = "0";
    string res_den = "0";
    string debugMessage = "";

    int selectedOperator = 0;
    string[] operatorTypes = new string[]
    {
        "+", "-", "*", "\u2044", ">", ">=", "<", "<=", "==", "!=", "CompareTo", "Equals"
    };
    
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        
        GUILayout.BeginVertical();
        a_num = EditorGUILayout.IntField(a_num, layoutOptions);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, layoutOptions); //https://forum.unity.com/threads/horizontal-line-in-editor-window.520812/
        a_den = EditorGUILayout.IntField(a_den, layoutOptions);
        GUILayout.EndVertical();
        
        GUILayout.BeginVertical();
        GUILayout.Space(20f);
        selectedOperator = EditorGUILayout.Popup(selectedOperator, operatorTypes, layoutOptions);
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        b_num = EditorGUILayout.IntField(b_num, layoutOptions);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, layoutOptions);
        b_den = EditorGUILayout.IntField(b_den, layoutOptions);
        GUILayout.EndVertical();
        
        GUILayout.BeginVertical();
        GUILayout.Space(20f);
        if (GUILayout.Button("=", layoutOptions))
        {
            Calculate();
        }
        GUILayout.EndVertical();
        
        GUILayout.BeginVertical();
        GUILayout.Label(res_num, layoutOptions);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, layoutOptions);
        GUILayout.Label(res_den, layoutOptions);
        GUILayout.EndVertical();
        
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
        GUILayout.Label(debugMessage);
    }

    private void Calculate()
    {
        if (a_den == 0 || b_den == 0)
        {
            debugMessage = "Fraction with denominator 0 detected!";
            return;
        }

        switch (selectedOperator)
        {
            case 0: //  +
                DisplayNumericResult(new RationalNumber(a_num, a_den) + new RationalNumber(b_num, b_den));
                break;
            case 1: //  -
                DisplayNumericResult(new RationalNumber(a_num, a_den) - new RationalNumber(b_num, b_den));
                break;
            case 2: //  *
                DisplayNumericResult(new RationalNumber(a_num, a_den) * new RationalNumber(b_num, b_den));
                break;
            case 3: //  /
                DisplayNumericResult(new RationalNumber(a_num, a_den) / new RationalNumber(b_num, b_den));
                break;
            case 4: //  >
                res_num = (new RationalNumber(a_num, a_den) > new RationalNumber(b_num, b_den)).ToString();
                res_den = "-";
                break;
            case 5: //  >=
                res_num = (new RationalNumber(a_num, a_den) >= new RationalNumber(b_num, b_den)).ToString();
                res_den = "-";
                break;
            case 6: //  <
                res_num = (new RationalNumber(a_num, a_den) < new RationalNumber(b_num, b_den)).ToString();
                res_den = "-";
                break;
            case 7: //  <=
                res_num = (new RationalNumber(a_num, a_den) <= new RationalNumber(b_num, b_den)).ToString();
                res_den = "-";
                break;
            case 8: //  ==
                res_num = (new RationalNumber(a_num, a_den) == new RationalNumber(b_num, b_den)).ToString();
                res_den = "-";
                break;
            case 9: //  !=
                res_num = (new RationalNumber(a_num, a_den) != new RationalNumber(b_num, b_den)).ToString();
                res_den = "-";
                break;
            case 10: // CompareTo
                res_num = new RationalNumber(a_num, a_den).CompareTo(new RationalNumber(b_num, b_den)).ToString();
                res_den = "-";
                break;
            case 11: // Equals
                res_num = new RationalNumber(a_num, a_den).Equals(new RationalNumber(b_num, b_den)).ToString();
                res_den = "-";
                break;
        }

        ClearDebugMessage();
    }

    private void DisplayNumericResult(RationalNumber rn)
    {
        res_num = rn.Numerator.ToString();
        res_den = rn.Denominator.ToString();
    }
    
    private void ClearDebugMessage()
    {
        debugMessage = "";
    }
}

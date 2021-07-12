using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [ContextMenu("RationalNumber Test")]
    private void TestRationalNumber()
    {
        RationalNumber num1 = new RationalNumber(120, 90);
        RationalNumber num2 = new RationalNumber(20, 15);
        RationalNumber num3 = new RationalNumber(15, 30);

        Debug.Log("num1: " + num1.ToString());
        Debug.Log("num2: " + num2.ToString());
        Debug.Log("num3: " + num3.ToString());
        
        
    }
}

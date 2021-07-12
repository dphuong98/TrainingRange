using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        TestRationalNumber();
    }

    [ContextMenu("RationalNumber Test")]
    private void TestRationalNumber()
    {
        // try
        // {
        //     RationalNumber errNum = new RationalNumber(1, 0);
        // }
        // catch (ArgumentException e)
        // {
        //     Debug.Log(e.Message);
        // }
        
        RationalNumber num1 = new RationalNumber(120, 90);
        RationalNumber num2 = new RationalNumber(20, 15);
        RationalNumber num3 = new RationalNumber(0, 30);

        Debug.Log("num1: " + num1.ToString() + '\n' + "num2: " + num2.ToString() + '\n' + "num3: " + num3.ToString());
        //Debug.Log("num1 == num2: " + num1.Equals(num2).ToString());
        
    }
}

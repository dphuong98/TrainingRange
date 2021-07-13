using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rn = RationalNumber;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        TestRationalNumber();
    }

    [ContextMenu("RationalNumber Test")]
    private void TestRationalNumber()
    {
        try
        {
            var errNum = new Rn(1, 0);
        }
        catch (ArgumentException e)
        {
            Debug.Log(e.Message);
        }
        
        var num1 = new Rn(120, 90);
        var num2 = new Rn(20, 15);
        var num3 = new Rn(0, 30);
        var num4 = -num1;

        var com1 = num1.CompareTo(num2);
        var com2 = num1.CompareTo(num3);
        var com3 = num4.CompareTo(num1);
        try
        {
            var com4 = new Rn(2, 1).CompareTo(2f);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        var com5 = new Rn(2, 1).CompareTo((object) new Rn(1, 2));

        var equ1 = num1.Equals(num2);
        var equ2 = num1.Equals(num3);
        var equ3 = new Rn(0, 20).Equals(new Rn(0, 10));
        var equ4 = num1.Equals(num4);

        var add1 = num1 + num2;
        var add2 = num1 + num3;
        var add3 = num1 + num4;

        var sub1 = num1 - num2;
        var sub2 = num1 - num3;
        var sub3 = num1 - num4;

        var mul1 = num1 * num2;
        var mul2 = num1 * num3;
        var mul3 = num1 * num4;

        var div1 = num1 / num2;
        try
        {
            var div2 = num1 / num3;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        var div3 = num1 / num4;

        var gt = num1 > num2;
        var gte = num1 >= num2;
        var lt = num1 < num2;
        var lte = num1 <= num2;

        var ilequ1 = num1 == num2;
        var ilequ2 = num1 == num3;
        var ilequ3 = num1 != num3;

        float f1 = num1;
        var rn1 = (Rn) f1;
        var rn2 = (Rn) 0.333333f;
        
        //TODO Handle overflow

        Debug.Log("Done");
    }
}

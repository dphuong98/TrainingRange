using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mathplus
{
    public static int GCD(int a, int b)
    {
        if (a == 0 || b == 0)
            throw new ArgumentException("Can not find greatest common divisor if one of the number is 0!");

        while (a != b)
        {
            if (a < b) b -= a;
            else a -= b;
        }

        return a;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mathplus
{
    public static int GCD(int a, int b)
    {
        while (a != b)
        {
            if (a < b) b -= a;
            else a -= b;
        }

        return a;
    }
}

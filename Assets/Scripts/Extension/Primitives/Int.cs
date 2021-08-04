using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Int
{
    private static readonly Random random = new Random();
    
    public static int Random(int max)
    {
        return random.Next(max);
    }
}

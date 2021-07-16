using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enumerable
{
    public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
    {
        // argument null checking omitted
        foreach(var item in sequence) action(item);
    }
}

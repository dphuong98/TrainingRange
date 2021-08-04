using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enumerable
{
    public static void ForEach<TSource>(this IEnumerable<TSource> sequence, Action<TSource> action)
    {
        // argument null checking omitted
        foreach(var item in sequence) action(item);
    }
    
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        var seenKeys = new HashSet<TKey>();
        foreach (var element in source)
        {
            if (seenKeys.Add(keySelector(element)))
            {
                yield return element;
            }
        }
    }
}

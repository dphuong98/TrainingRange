using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class String : MonoBehaviour
{
    public static string JoinFilter(string separator, IEnumerable<string> strings)
    {
        return string.Join(separator, strings.Where(s => !string.IsNullOrEmpty(s)));
    }
    public static string JoinFilter(string separator, params string[] str)
    {
        return string.Join(separator, str?.Where(s => !string.IsNullOrEmpty(s)) ?? Array.Empty<string>());
    }
}

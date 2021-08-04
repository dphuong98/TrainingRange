using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

public class String
{
    public static string JoinFilter(string separator, IEnumerable<string> strings)
    {
        return string.Join(separator, strings.Where(s => !string.IsNullOrEmpty(s)));
    }
    public static string JoinFilter(string separator, params string[] str)
    {
        return string.Join(separator, str?.Where(s => !string.IsNullOrEmpty(s)) ?? Array.Empty<string>());
    }

    private static readonly Random random = new Random();
    public static string Random(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[length];
        for (var i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
}

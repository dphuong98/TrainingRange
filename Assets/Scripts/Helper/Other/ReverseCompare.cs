using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseCompare<T> : IComparer<T> where T: IComparable<T>
{
    public int Compare(T x, T y)
    {
        return -x.CompareTo(y);
    }
}

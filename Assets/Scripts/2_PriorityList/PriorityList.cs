using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PriorityList<T> : ICollection<T> where T : IComparable<T>
{
    private List<T> data;
    
    public PriorityList()
    {
        data = new List<T>();
    }

    public int Count => data.Count;
    public bool IsReadOnly => ((IList<T>) data).IsReadOnly;

    public IEnumerator<T> GetEnumerator()
    {
        return data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        if (item == null)
            return;
        
        if (Count == 0 || item.CompareTo(data.Last()) <= 0)
        {
            data.Add(item);
            return;
        }

        var index = data.BinarySearch(item, new ReverseCompare<T>());
        if (index >= 0)
            data.Insert(index, item);
        else
            data.Insert(-(index+1), item);
    }

    public void Clear()
    {
        data.Clear();
    }

    public bool Contains(T item)
    {
        return data.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        data.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        return data.Remove(item);
    }
}

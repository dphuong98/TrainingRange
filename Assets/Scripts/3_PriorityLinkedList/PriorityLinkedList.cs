using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;
using UnityEngine;

public class PriorityLinkedList<T> :  IEnumerable<T>, ICollection<T>  where T : IComparable<T>
{
    private LinkedList<T> data;
    
    public PriorityLinkedList()
    {
        data = new LinkedList<T>();
    }

    public int Count => data.Count;
    public bool IsReadOnly => ((ICollection<T>) data).IsReadOnly;

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
            data.AddLast(item);
            return;
        }

        //TODO Implement a faster search algorithm or just linear search. Performance prolly is not that important, just flexing
        //https://www.geeksforgeeks.org/searching-algorithms/
        var node = data.BinarySearch(item, new ReverseCompare<T>());
        data.AddBefore(node, item);
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
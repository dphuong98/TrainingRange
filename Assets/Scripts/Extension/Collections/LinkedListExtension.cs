using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LinkedListExtension
{
    private static LinkedListNode<T> MiddleNode<T>(LinkedListNode<T> start, LinkedListNode<T> last)
    {
        if (start == null)
            return null;

        var slow = start;
        var fast = start.Next;

        while (fast != last)
        {
            fast = fast.Next;
            if (fast != last)
            {
                slow = slow.Next;
                fast = fast.Next;
            }
        }

        return slow;
    }
    
    public static LinkedListNode<T> BinarySearch<T>(this LinkedList<T> data, T item) where T : IComparable<T>
    {
        var start = data.First;
        LinkedListNode<T> last = null;

        do
        {
            var mid = MiddleNode(start, last);

            if (mid == null)
                return null;

            if (mid.Value.CompareTo(item) == 0)
                return mid;
            
            if (mid.Value.CompareTo(item) < 0)
            {
                start = mid.Next;
            }
            else
                last = mid;
            
        } while (last == null || last != start);

        return last;
    }
    
    public static LinkedListNode<T> BinarySearch<T>(this LinkedList<T> data, T item, IComparer<T> comparer)
    {
        var start = data.First;
        LinkedListNode<T> last = null;

        do
        {
            var mid = MiddleNode(start, last);

            if (mid == null)
                return null;

            if (comparer.Compare(mid.Value, item) == 0)
                return mid;
            
            if (comparer.Compare(mid.Value, item) < 0)
            {
                start = mid.Next;
            }
            else
                last = mid;
            
        } while (last == null || last != start);

        return last;
    }
}

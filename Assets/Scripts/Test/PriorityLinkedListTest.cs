using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class PriorityLinkedListTest : MonoBehaviour
{
    private Random rand;

    [ContextMenu("PriorityLinkedList test")]
    private void Start()
    {
        rand = new Random();
        
        var integers = new PriorityLinkedList<int>();
        for (var i = 0; i < 10; i++)
        {
            integers.Add(rand.Next(100));
        }
        Debug.Log(string.Join(", ", integers));
        
        var strings = new PriorityLinkedList<string>();
        for (var i = 0; i < 10; i++)
        {
            strings.Add(String.Random(5));
        }
        Debug.Log(string.Join(", ", strings));
    }
}

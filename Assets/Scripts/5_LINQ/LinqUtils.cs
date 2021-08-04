using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class LinqUtils
{
    public static IEnumerable<IEnumerable<Data>> FindDuplicates(IEnumerable<Data> entries)
    {
        return entries.GroupBy(item => item);
    }

    public static IEnumerable<IEnumerable<string>> FindKeysOfDuplicatedValue(IEnumerable<Data> entries)
    {
        return entries.GroupBy(item => item.value)
            .Select(s => s.Select(v => v.key).ToList());
    }

    public static Dictionary<string, List<Data>> FindDataWithDuplicatedKey(IEnumerable<Data> entries)
    {
        return entries.GroupBy(item => item.key)
            .ToDictionary(s => s.Key, s => s.Select(v => v).ToList());
    }

    public static IEnumerable<Data> UniqueKey(IEnumerable<Data> entries)
    {
        return entries.DistinctBy(item => item.key);
    }

    public static Dictionary<string, int> ToDictionary(IEnumerable<Data> entries)
    {
        return entries.ToDictionary(item => item.key, item => item.value);
    }

    public static IEnumerable<Data> UniqueKey(IEnumerable<Category> entries)
    {
        return UniqueKey(entries.Select(item => item.entries)
            .Aggregate((s1, s2) => s1.Concat(s2).ToList()));
    }

    public static Dictionary<string, int> ToDictionary(IEnumerable<Category> entries)
    {
        return entries.Select(item => item.entries)
            .Aggregate((s1, s2) => s1.Concat(s2).ToList())
            .ToDictionary(item => item.key, item => item.value);
    }
}

public struct Data
{
    public string key;
    public int value;
}

public struct Category
{
    public string name;
    public List<Data> entries;
}
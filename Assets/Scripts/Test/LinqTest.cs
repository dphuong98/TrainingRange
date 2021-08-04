using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinqTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var dataGroup = GenerateDataList();
        var dataDupGroup = GenerateDataListWithDups();
        var categories = GenerateCategories();
        var categoriesDup = GenerateCategoriesWithDup();

        var findDups = LinqUtils.FindDuplicates(dataDupGroup).ToList();
        var findKeysOfDupValue = LinqUtils.FindKeysOfDuplicatedValue(dataDupGroup).ToList();
        var findDataWithDupKeys = LinqUtils.FindDataWithDuplicatedKey(dataDupGroup);
        
        var dataUniqueKey = LinqUtils.ToDictionary(LinqUtils.UniqueKey(dataDupGroup));
        var dataDictionary = LinqUtils.ToDictionary(dataGroup);
        var categoryUniqueKey = LinqUtils.ToDictionary(LinqUtils.UniqueKey(categoriesDup));
        var categoryDictionary = LinqUtils.ToDictionary(categories);

        var done = true;
    }

    private List<Data> GenerateDataList()
    {
        var dataGroup = new List<Data>();
        for (var i = 0; i < 10; i++)
        {
            var newData = new Data {key = String.Random(7), value = Int.Random(100000)};
            dataGroup.Add(newData);
        }
        return dataGroup;
    }
    
    private List<Data> GenerateDataListWithDups()
    {
        var dataGroup = new List<Data>();
        for (var i = 0; i < 10; i++)
        {
            var newData = new Data {key = String.Random(7), value = Int.Random(100000)};
            dataGroup.Add(newData);
            if (i % 2 == 0)
                dataGroup.Add(newData);
        }
        return dataGroup;
    }

    private List<Category> GenerateCategories()
    {
        var categories = new List<Category>();
        for (var i = 0; i < 10; i++)
        {
            categories.Add(new Category() {name = String.Random(7), entries = GenerateDataList()});
        }
        return categories;
    }
    
    private List<Category> GenerateCategoriesWithDup()
    {
        var categories = new List<Category>();
        for (var i = 0; i < 10; i++)
        {
            var newEntries = GenerateDataList();
            categories.Add(new Category() { name = String.Random(7), entries = newEntries });
            if (i % 2 == 0)
                categories.Add(new Category() { name = String.Random(7), entries = newEntries });
        }
        return categories;
    }
}

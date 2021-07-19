using UnityEngine;
using Random = System.Random;

public class PriorityListTest : MonoBehaviour
{
    private PriorityList<int> integers;
    
    [ContextMenu("PriorityList test")]
    // Start is called before the first frame update
    void Start()
    {
        var rand = new Random();
        
        integers = new PriorityList<int>();
        for (var i = 0; i < 10; i++)
        {
            integers.Add(rand.Next(8));
        }
        Debug.Log(string.Join(", ", integers));
        
        var strings = new PriorityList<string>();
        for (var i = 0; i < 10; i++)
        {
            strings.Add(String.Random(5));
        }
        Debug.Log(string.Join(", ", strings));
    }
}

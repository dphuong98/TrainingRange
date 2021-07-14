using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testspam : MonoBehaviour
{
    public Vector3 lookAtPoint = Vector3.zero;
    
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAtPoint);
    }
}

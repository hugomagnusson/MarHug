using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var lifetime = 10f;
        Destroy(gameObject, lifetime);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

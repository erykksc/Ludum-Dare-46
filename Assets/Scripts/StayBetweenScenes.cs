using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayBetweenScenes : MonoBehaviour
{
    private static bool exists = false;
    void Awake()
    {
        if(exists)
        {
            Destroy(gameObject);
            return;
        }
        exists = true;
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

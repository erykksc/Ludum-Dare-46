using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform followed;
    [SerializeField] int size;
    [SerializeField] int tightness;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = followed.position-transform.position;
        dir.z = dir.y = 0;
        //dir.y = dir.z = 0;
        if(dir.magnitude>size)
        {
            dir = (transform.position)*(1-Time.fixedDeltaTime*tightness) + (followed.position-dir.normalized*size)*Time.fixedDeltaTime*tightness;
            dir.z = transform.position.z;
            dir.y = transform.position.y;
            transform.position = dir;
        }
    }
}

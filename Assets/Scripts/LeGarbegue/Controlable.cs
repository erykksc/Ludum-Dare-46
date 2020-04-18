using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);
        transform.position += dir*Time.fixedDeltaTime*speed;
    }
}

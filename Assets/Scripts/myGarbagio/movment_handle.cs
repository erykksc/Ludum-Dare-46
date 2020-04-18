using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment_handle : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (Input.GetKey("w")) {
            rb.AddForce(transform.forward * speed);
        }
        if (Input.GetKey("s")) {
            rb.AddForce(-transform.forward * speed);
        }
        if (Input.GetKey("d")) {
            rb.AddForce(transform.right * speed);
        }
        if (Input.GetKey("a")) {
            rb.AddForce(-transform.right * speed);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_platform : Activatable
{
    [SerializeField]
    private Vector2 force;
    [SerializeField]
    private float maxSpeed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (active) {
            rb.AddForce(force);
        }
    }
}

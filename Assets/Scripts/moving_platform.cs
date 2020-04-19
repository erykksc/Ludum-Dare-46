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
    Vector2 GetDragVector() {
        return -rb.velocity;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (active) {
            rb.AddForce(force);
        }
        rb.AddForce(GetDragVector());
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        force = -force;
        rb.velocity = new Vector2();
    }
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        if (active) {
            DrawArrow.ForGizmo(transform.position, force);
        }
    }
}

/*
    moving_platform : Activatable
        - Co robi:
            porusza sobą prawo/lewo jeśli jest aktywna
        - Na czym powinien być:
            tak
        - Jakich komponentów wymaga:
            - rigidbody2D
            - Collider2D jako trigger
        - Specjalne Ustawienia
            - force (siła)
            - dragFactor (siła tarcia na platformę)

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_platform : Activatable
{
    [SerializeField]
    private Vector2 force;
    [SerializeField]
    private float drag_factor;
    private Rigidbody2D rb;
    Vector2 GetDragVector() {
        return -rb.velocity*drag_factor;
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
    void OnDrawGizmos()
    {
        if (active) {
            DrawArrow.ForGizmo(transform.position, force);
        }
    }
}

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
    private float force;
    [Tooltip("Podawać raw cordy, ponieważ pierdolić usera")]
    public Vector2[] points;
    public int PointToGoTo;
    [SerializeField]
    private float tolerance;
    [SerializeField]
    private float drag_factor;
    private Rigidbody2D rb;
    private Vector2 GetDragVector() {
        return -rb.velocity*drag_factor;
    }
    private Vector2 GetMovmentVector() {
        var pos2D = new Vector2(transform.position.x, transform.position.y);
        if ((pos2D - points[PointToGoTo]).magnitude < tolerance) {
            rb.velocity = new Vector2();
            if (PointToGoTo < points.Length - 1) {
                PointToGoTo++;
            }
            else {
                PointToGoTo = 0;
            }
        }
        var res = (points[PointToGoTo] - pos2D).normalized * force;
        return res;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (active) {
            rb.AddForce(GetMovmentVector());
        }
        rb.AddForce(GetDragVector());
    }
    void OnDrawGizmos()
    {
        for (int i = 1; i < points.Length; i++)
        {
            var point1 = new Vector3(points[i-1].x, points[i-1].y, 0);
            var point2 = new Vector3(points[i].x, points[i].y, 0);
            Gizmos.DrawLine(point1, point2);
        }
        var point11 = new Vector3(points[points.Length - 1].x, points[points.Length - 1].y, 0);
        var point21 = new Vector3(points[0].x, points[0].y, 0);
        Gizmos.DrawLine(point11, point21);
        Gizmos.DrawWireSphere(transform.position, tolerance);
    }
}

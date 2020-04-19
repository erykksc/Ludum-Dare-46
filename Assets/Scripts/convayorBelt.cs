using UnityEngine;

public class convayorBelt : Activatable
{
    public float xSpeed;
    private float topHeight;

    private void OnCollisionStay2D(Collision2D other) {
        if(! active)
            return;
        Rigidbody2D rb = other.rigidbody;
        if (rb == null)
            return;

        // Check if collision is horizontal(-ish)
        Vector2 normal = other.contacts[0].normal;
        Debug.Log(normal);
        if( Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            return;

        //Apply force
        Vector2 force = Vector2.right * xSpeed * 100f * rb.mass;
        rb.AddForce(force * Time.deltaTime, ForceMode2D.Impulse);
    }

}
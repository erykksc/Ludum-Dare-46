using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public int base_damage;
    public float special_multiplier;
    private Rigidbody rb;
    public float speed_bonus;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "enemy") {
            // generate slowdown force
            var enemy = other.gameObject.GetComponent<enemy>();
            rb.AddForce(-rb.velocity*enemy.slowdownFactor);
            // calculate damage
            int damage = base_damage;
            float multiplied = base_damage * Mathf.Pow(speed_bonus, other.relativeVelocity.magnitude);
            damage = (int) multiplied;
            Debug.Log(damage);
            enemy.DealDamage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

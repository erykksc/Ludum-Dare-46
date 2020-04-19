/*
SimpleEnemy
    - Co robi:
        - Chodzi w lewo, uderza w coś, idzie w prawo.
    - Na czym powinien być:
        - na prostym przeciwniku
    - Jakich komponentów wymaga:
        - Rigidbody2D
        - Collider2D ustawiony jako trigger
    - Specjalne ustawienia:
        - Force: wektor siły
        - maxSpeed: prędkość maksymalna
        - Reszta dziedziczona po Character

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : Character
{
    [SerializeField]
    private Vector2 Force;
    [SerializeField]
    private float maxSpeed;
    private Rigidbody2D rigidbody;
    [SerializeField]
    private float damage;
    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();    
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude < maxSpeed) {
            rigidbody.AddForce(Force);
        }
    }
    
    void CheckAndDamage(GameObject collisionGameObject){
        if (collisionGameObject.tag == "Player" || collisionGameObject.tag == "Kid") {
            collisionGameObject.GetComponent<Character>().dealDamage(damage);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Stop self and reverse direction
        Force.x = - Force.x;
        CheckAndDamage(other.gameObject);
    }
    void OnDrawGizmos()
    {
        DrawArrow.ForGizmo(transform.position, Force);
    }
    void OnCollisionEnter2D(Collision2D other) {
        CheckAndDamage(other.gameObject);
    }
}

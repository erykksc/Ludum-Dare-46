using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Damaging - add it to a game object and set its damage and knockback. It will hurt the player and knock him back.
 * 
 * Simple enemy derives from it and adds MOVEMENT
 * 
 */


public class Damaging : Character
{
    [Header("damage - Damaging class property")]
    [SerializeField] public int damage;
    [SerializeField] public float knockback_strength =2;
    [SerializeField] public Vector2 knockback_direction;

    public void CheckAndDamage(GameObject collisionGameObject)
    {
        if (collisionGameObject.tag == "Player" || collisionGameObject.tag == "Kid")
        {
            collisionGameObject.GetComponent<Character>().dealDamage(damage);
            GameObject.Find("LevelManager").GetComponentInChildren<UI_Handler>().SetHealthBar(collisionGameObject.gameObject.GetComponent<Character>().HP);
            Debug.Log("damaging. HP is now: " + collisionGameObject.gameObject.GetComponent<Character>().HP);
            KnockBack(collisionGameObject);
        }
    }
    public void KnockBack(GameObject reciever)
    {
        knockback_direction = (reciever.transform.position - transform.position).normalized;
        reciever.GetComponent<Rigidbody2D>().AddForce(knockback_direction * knockback_strength,ForceMode2D.Impulse);

    }



    void OnCollisionEnter2D(Collision2D other)
    {
        CheckAndDamage(other.gameObject);
    }
}

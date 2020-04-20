using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpboost : Activatable
{
    public float jumpForce;
    Collider2D selfCollider;

    private void OnTriggerEnter2D(Collider2D other) {
        string tag = other.tag;
        if(tag == "Player"){
            Rigidbody2D playerRB = other.attachedRigidbody;
            Vector2 force = Vector2.up* jumpForce * playerRB.mass * 60f;

            playerRB.AddForce(force, ForceMode2D.Force);
        }
    }

    private void Awake() {
        selfCollider = GetComponent<Collider2D>();
    }

    override public void activate(){
        selfCollider.enabled = true;
    }
    override public void de_activate(){
        selfCollider.enabled = false;
    }
}

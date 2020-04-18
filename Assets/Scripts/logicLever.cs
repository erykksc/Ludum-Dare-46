using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicLever : MonoBehaviour
{
/*
    Używaj tego componentu, dla każdej dźwigni w scenie
    dźwignie automatycznie znajdą i połączą się z gatem
    po uruchomieniu gry

    Requirements:
    - rigidbody in player
    - tryingToInteract must be implemented in player
*/

    //public Rigidbody2D rb;

    public bool state ; 
    public bool targetState;

    public int id = -1;

    private bool colliding = false;
    
    private logicGate gate; //Find only gate in level
    void Start()
    {
        gate = FindObjectOfType<logicGate>();
        if (gate)
            Debug.Log("Gate found");
    }

    public bool isEnabled(){
        return state == targetState ? true : false;
    }

    void flipSwitch() {
        state =! state;
        gate.setState(id, isEnabled());
        Debug.Log("Flip switch");
    }


    bool tryingToInteract(Collider2D collider){
    // Check if player is trying to interact
        if (isCollider("Player", collider))
            return collider.gameObject.GetComponent<Player>().tryingToInteract;
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        bool interact = tryingToInteract(other);
        if(interact){
            flipSwitch();
            var player = other.gameObject.GetComponent<Player>();
            player.GetComponent<Player>().tryingToInteract = false;
        }
    }

    bool isCollider(string tag, Collider2D collider){
        return collider.gameObject.tag == tag ? true : false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicInput : MonoBehaviour
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

    [SerializeField]
    private logicGate gate; 

    [SerializeField]
    private float leverPlayerDistance = 1.1f;
    private float sqrLPDist;

    bool inCollision;
    Collider2D col ;



    private void Update() {
        if (inCollision){
            Vector3 playerPos = col.gameObject.transform.position;
            Vector3 leverPos = transform.position;

            // Nasty hack
            if(Vector3.SqrMagnitude(playerPos - leverPos) > sqrLPDist){
                inCollision = false;
                return;
            }
            try2Interact(col);

        }
    }


    void Start()
    {
        sqrLPDist = leverPlayerDistance * leverPlayerDistance;
    }

    public bool isEnabled(){
        return state == targetState ? true : false;
    }

    void flipSwitch() {
        state =! state;
        gate.updateState();
        Debug.Log("Flip switch");
    }


    bool isTryingToInteract(Collider2D collider){
    // Check if player is trying to interact
        if (isCollider("Player", collider))
            return collider.gameObject.GetComponent<Player>().tryingToInteract;
        return false;
    }



    void try2Interact(Collider2D other){
        inCollision = true;
        bool interact = isTryingToInteract(other);
        if(interact){
            flipSwitch();
            var player = other.gameObject.GetComponent<Player>();
            player.GetComponent<Player>().tryingToInteract = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        col = other;
        
        ContactPoint2D[] contacts = {};
        var a = other.GetContacts(contacts);
        Debug.Log("Contacts : "+a.ToString());
        try2Interact(other);
    }
 

    bool isCollider(string tag, Collider2D collider){
        return collider.gameObject.tag == tag ? true : false;
    }
}

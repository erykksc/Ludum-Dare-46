using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicInput : MonoBehaviour
{
/*
    Base class for all switches (inputs)

    Requirements:
    - rigidbody in player
    - tryingToInteract must be implemented in player
*/

    //public Rigidbody2D rb;

    public bool state ; 
    public bool targetState = true;

    public logicGate gate; 

    [SerializeField]
    private float leverPlayerDistance = 1.1f;
    private float sqrLPDist;

    bool inCollision;
    Collider2D col ;

    // Methods to override :
    virtual public void onAwake(){} // Don't override onAwake(), use this instead
    virtual public void onPlayerTouchStart(){}
    virtual public void onPlayerTouchEnd(){}
    virtual public void onCorrect(){}
    virtual public void onIncorrect(){}
    virtual public void onInteraction(){}





    private void Update() {
        if (inCollision){
            Vector3 playerPos = col.gameObject.transform.position;
            Vector3 leverPos = transform.position;

            // Nasty hack
            if(Vector3.SqrMagnitude(playerPos - leverPos) > sqrLPDist){
                onPlayerTouchEnd();
                inCollision = false;
                return;
            }
            try2Interact(col);
        }
    }


    void Awake()
    {
        sqrLPDist = leverPlayerDistance * leverPlayerDistance;
        onAwake();
    }

    public bool isCorrect(){
        return state == targetState ? true : false;
    }

    void flipSwitch() {
        onInteraction();

        if(isCorrect())
            onCorrect();
        else
            onIncorrect();
    }


    bool isTryingToInteract(Collider2D collider){
    // Check if player is trying to interact
        if (hasColliderTag("Player", collider))
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
        onPlayerTouchStart();
        try2Interact(other);
    }
 

    bool hasColliderTag(string tag, Collider2D collider){
        return collider.gameObject.tag == tag ? true : false;
    }

    public void updateGate(){
        gate.updateState();
    }

    
}

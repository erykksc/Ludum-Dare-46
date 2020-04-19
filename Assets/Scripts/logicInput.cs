using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicInput : MonoBehaviour
{
/*
    Base class for all switches (inputs)
    see existing subclasses for examples on usage

    Requirements:
    - 2D collider on input
    - rigidbody in player
    - tryingToInteract must be implemented in player
*/


    // Methods to override :
    virtual public void onAwake(){} // Don't override onAwake(), use this instead
    virtual public void onPlayerTouchStart(){}
    virtual public void onPlayerTouchEnd(){}
    virtual public void onCorrect(){}
    virtual public void onIncorrect(){}
    virtual public void onInteraction(){}


    public bool state;
    public bool targetState = true;

    public logicGate gate; 
    
    [SerializeField,Tooltip("The maximum distance from the switch to the player")] 
    private float leverPlayerDistance = 1.1f;
    private float sqrLPDist;
    bool inCollision;
    Collider2D col;



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

    void try2Interact(Collider2D other){
        inCollision = true;
        bool interact = isPlayerTryingToInteract(other);
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

    bool isPlayerTryingToInteract(Collider2D collider){
    // Check if player is trying to interact
        if (hasColliderTag("Player", collider))
            return collider.gameObject.GetComponent<Player>().tryingToInteract;
        return false;
    }
    
    public void updateGate(){
        gate.updateState();
    }

    void Awake()
    {
        sqrLPDist = leverPlayerDistance * leverPlayerDistance;
        onAwake();
    }
}

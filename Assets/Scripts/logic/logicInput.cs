using System;
using System.Collections.Generic;
using UnityEngine;

abstract public class logicInput : MonoBehaviour
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
    virtual public void onKidTouchStart(){}
    virtual public void onKidTouchEnd(){}
    virtual public void onCorrect(){}
    virtual public void onIncorrect(){}
    virtual public void onInteraction(){}


    public bool state;
    public bool targetState = true;
    public logicGate gate; 
    
    [SerializeField,Tooltip("The maximum distance from the switch to the player")] 
    private float leverPlayerDistance = 1.1f;
    private float sqrLPDist;
    [NonSerialized] public bool inCollisionPlayer;
    [NonSerialized] public bool inCollisionKid;

    [NonSerialized] public Collider2D collisionPlayer;
    [NonSerialized] public Collider2D collisionKid;

    [NonSerialized] string kidTag = "Kid";
    [NonSerialized] string playerTag = "Player";



    private void Update() {
        Vector3 inputPos = transform.position;
        if (inCollisionPlayer){
            Vector3 playerPos = collisionPlayer.gameObject.transform.position;

            // Nasty hack
            if(Vector3.SqrMagnitude(playerPos - inputPos) > sqrLPDist){
                inCollisionPlayer = false;
                onPlayerTouchEnd();
                return;
            }
            try2Interact(collisionPlayer);
        }

        if(inCollisionKid){
            Vector3 kidPos = collisionKid.gameObject.transform.position;
            
            if(Vector3.SqrMagnitude(kidPos - inputPos) > sqrLPDist){
                inCollisionKid = false;
                onKidTouchEnd();
                return;
            }
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
        inCollisionPlayer = true;
        bool interact = isPlayerTryingToInteract(other);
        if(interact){
            flipSwitch();
            var player = other.gameObject.GetComponent<Player>();
            player.GetComponent<Player>().tryingToInteract = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        string tag = other.gameObject.tag;
        if(tag == kidTag){
            collisionKid = other;
            inCollisionKid = true;
            onKidTouchStart();
        }
        if(tag == playerTag){
            collisionPlayer = other;
            inCollisionPlayer = true;
            onPlayerTouchStart();
        }
        try2Interact(other);
    }
 
    public bool hasColliderTag(string tag, Collider2D collider){
        return collider.gameObject.tag == tag ? true : false;
    }
    
    public virtual bool isPlayerTryingToInteract(Collider2D collider){
        // Check if player is trying to interact
        string tag = collider.gameObject.tag;
        if (tag == playerTag)
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
        //Idiotproofness
        var collider = GetComponent<Collider2D>();
        if (collider == null)
            Debug.LogException(new UnityException("A collider2D comonent is required"));
        if (collider.isTrigger == false)
            Debug.LogException(new UnityException("collider2D comonent must be trigger"));
    }
}

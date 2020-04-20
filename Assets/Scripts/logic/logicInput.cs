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

    [SerializeField,Tooltip("If you dont want to add this to gate when starting game")]
    public logicGate gate; 
    
    [SerializeField,Tooltip("The maximum distance from the switch to the player")] 
    private float leverPlayerDistance = 1.6f;
    private float sqrLPDist;
    [NonSerialized] public bool inCollisionPlayer;
    [NonSerialized] public bool inCollisionKid;

    [NonSerialized] string kidTag = "Kid";
    [NonSerialized] string playerTag = "Player";

    static Player cache_player;



    private void OnTriggerStay2D(Collider2D other) {
        string tag = other.tag;
        if(inCollisionPlayer){
            try2Interact();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        string tag = other.tag;
        if (tag == playerTag){
            inCollisionPlayer = false;
            onPlayerTouchEnd();
            try2Interact();
            return;
        }
        if(tag == kidTag){
            inCollisionKid = false;
            onKidTouchEnd();
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

    void try2Interact(){
        //inCollisionPlayer = true;
        bool interact = isPlayerTryingToInteract();
        if(interact){
            cache_player.tryingToInteract = false;
            flipSwitch();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        string tag = other.gameObject.tag;
        if(tag == kidTag){
            inCollisionKid = true;
            onKidTouchStart();
        }
        if(tag == playerTag){
            cache_player = other.gameObject.GetComponent<Player>();
            inCollisionPlayer = true;
            onPlayerTouchStart();
            try2Interact();
        }
        
    }

    
    public virtual bool isPlayerTryingToInteract(){
        // Check if player is trying to interact
        return cache_player.tryingToInteract;
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
        
        logicInput self = gameObject.GetComponent<logicInput>();
        lock (gate.inputs)
        {
            if(gate.inputs == null)
                gate.inputs = new List<logicInput>();
            gate.inputs.Add(self);
            
        }
    }

}

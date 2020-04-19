using System;
using UnityEngine;

public class logicPressurePlate : logicInput
{
    //Upwenij się, że ten komponent jest jedny z logicInput na objekcie

    void onTouchStart(){
        if ( inCollisionKid || inCollisionPlayer )
            state = targetState;
        updateGate();
    }

    void onTouchEnd(){
        if(! (inCollisionKid || inCollisionPlayer))
            state = !targetState;
        updateGate();
    }

    private void Start() {
        targetState = true;
        state = !targetState;
    }

    override public void onPlayerTouchStart(){ onTouchStart(); }
    override public void onKidTouchStart()   { onTouchStart(); }
    override public void onPlayerTouchEnd()  { onTouchEnd();   }
    override public void onKidTouchEnd()     { onTouchEnd();   }

    override public bool isPlayerTryingToInteract(Collider2D collider){
        // Check if player is trying to interact
        string tag = collider.gameObject.tag;
        if (tag == "Player")
            return collider.gameObject.GetComponent<Player>().tryingToInteract;
        if (tag == "Kid")
            return true;
        return false;
    }

}
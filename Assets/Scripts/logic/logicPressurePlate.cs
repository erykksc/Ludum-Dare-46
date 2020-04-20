using System;
using UnityEngine;

public class logicPressurePlate : logicInput
{
    //Upwenij się, że ten komponent jest jedny z logicInput na objekcie

    void onTouchStart(){
        if ( inCollisionKid || inCollisionPlayer)
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

}
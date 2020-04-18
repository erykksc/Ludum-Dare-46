using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicLever : logicInput
{
/*
    Upwenij się, że ten komponent jest jedny z logicInput na objekcie
    Używaj tego componentu, dla każdej dźwigni w scenie
    
    Requirements:
    - rigidbody in player
    - tryingToInteract must be implemented in player
*/
    override public void onPlayerTouchStart(){
        Debug.Log("Player Touch start");
    }
    override public void onPlayerTouchEnd(){
        Debug.Log("Player Touch end");
    }

    override public void onInteraction(){
        state = !state;
        updateGate();
    }


}

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

    override public void onInteraction(){
        state = !state;
        updateGate();
    }
}

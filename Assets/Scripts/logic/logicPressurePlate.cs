using UnityEngine;

public class logicPressurePlate : logicInput
{
    //Upwenij się, że ten komponent jest jedny z logicInput na objekcie
    private void Start() {
        targetState = true;
        state = !targetState;
    }


    override public void onPlayerTouchStart(){
        Debug.Log("Player Touch start");
        state = targetState;
        updateGate();
    }
    override public void onPlayerTouchEnd(){
        Debug.Log("Player Touch end");
        state = !targetState;
        updateGate();
    }
}
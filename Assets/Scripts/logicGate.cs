using UnityEngine;

public class logicGate : MonoBehaviour
{
    /*
        Gate is activated if all levers are in correct positions

        Use this components 'isActivated' variable, to check the state of the gate

        Light game logic implementation
        the state of the gate is updated only on lever updates
    */
    

    private bool[] states;
    private int num ;
    
    private bool prev_action;


    public bool isActivated;

    void Open(){
        isActivated = true;
    }

    void Close(){
        isActivated = false;
    }


    public logicInput[] levers;


    public void updateState(){
        // Fire Open/Close if all levers start/stop being correctly set
        bool allEnabled = true;
        foreach(var l in levers){
            if( l.isEnabled() == false){
                allEnabled = false;
                break;
            }
        }

        bool action = allEnabled;

        if (action != prev_action)
            if(action)
                Open();
            else
                Close();

        prev_action = action;
        
        action = allEnabled;

    }

}

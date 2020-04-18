using UnityEngine;

public class logicGate : MonoBehaviour
{
    private bool[] states;
    private int num ;
    
    private Component target;
    private bool prev_action;

    void turnOn(){}
    void turnOff(){}

    public void setState(int id, bool state){
        states[id] = state;
        bool allEnabled = true;
        for(int i = 0; i<num; i++)
            if (states[i] == false){
                allEnabled = false;
                break;
            }
        
        bool action = allEnabled;

        if (action != prev_action)
            if (action)
                turnOn();
            else
                turnOff();
        prev_action = action;
    }


    void Start()
    {
        logicLever[] levers = FindObjectsOfType<logicLever>();
        int i = 0;
        foreach (var lever in levers){
            states[i] = lever.isEnabled();
            lever.id = i++;
        }
        num = i;
    }

}

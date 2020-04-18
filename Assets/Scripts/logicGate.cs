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



    public void setState(int id, bool state){
        if (id == -1){
            Debug.Log("Request from non-initialized level");
            return;
        }

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
                Open();
            else
                Close();
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

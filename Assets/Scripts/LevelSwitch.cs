using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class inheriting logic gate, if all required activateables are activated, sets bool in player, allowing him to move to next level
// Class detects player on begining of the scene and checks wheather nex level can be accessed
public class LevelSwitch : logicGate
{
    // Start is called before the first frame update
    [SerializeField] Player player;
    void Start()
    {
        int count = Resources.FindObjectsOfTypeAll<Player>().Length;
        if(count>0)
        {
            player = Resources.FindObjectsOfTypeAll<Player>()[0];
        }
        openFn = OnNextLevel;
        closeFn = DoNothing;
    }
    void DoNothing()
    {

    }
    void OnNextLevel()
    {
        player.clearedLevel = true;
        Debug.Log("Invoked");
    }
    void Update()
    {
        updateState();
    }
    // Update is called once per frame
}

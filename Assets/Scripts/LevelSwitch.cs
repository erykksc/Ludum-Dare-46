using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class inheriting logic gate, if all required activateables are activated, sets bool in player, allowing him to move to next level
public class LevelSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    void Start()
    {
        int count = Resources.FindObjectsOfTypeAll<Player>().Length;
        if(count>0)
        {
            player = Resources.FindObjectsOfTypeAll<Player>()[0];
        }
    }
    void OnNestLevel()
    {
        player.clearedLevel = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

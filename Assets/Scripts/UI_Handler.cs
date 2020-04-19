using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{
    public Transform healthbar;
    public Sprite heart_full;
    public Sprite heart_empty;


    public void SetHealthBar(int health)
    {
        for (int i = 4; i > -1; i--)
        {
            if (health < i+1) { Debug.Log("oof" + i); healthbar.GetChild(i).GetComponent<Image>().sprite = heart_empty;  }

        }


    }
}

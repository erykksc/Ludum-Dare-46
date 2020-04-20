using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{
    public Transform healthbar;
    public Sprite heart_full;
    public Sprite heart_empty;
    


    public void SetHealthBar(int health)
    {
        for(int i = 0;i<5;i++)
        {
            healthbar.GetChild(i).GetComponent<Image>().sprite = heart_empty;
        }
        for(int i = 0;i<health;i++)
        {
            healthbar.GetChild(i).GetComponent<Image>().sprite = heart_full;
        }


    }
}

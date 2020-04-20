using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] LevelManager manager;
    public GameObject Credits_panel;
    public Text credits_text;

    public void Awake()
    {
        manager.GetComponentInChildren<UI_Handler>().gameObject.GetComponent<Canvas>().enabled = false;
        credits_text.text =
            "Andrzej Kominek- level design, coding \n" +
            "Eryk Kściuczyk - coding \n" +
            "Grzegorz Koperwas - coding  \n" +
            "Mikołaj Czarnecki - animations, coding  \n" +
            "Maja Oszczypała - graphics  \n" +
            "Maja Więzik - graphics  \n" +
            "Michał Danikiewicz - Story  \n" +
            "Kamil Kowalski - coding" 
            ;
    }

    //Load lvl 1 on press
    public void PlayButtonPress()
    {
        // manager.setLevel(1);
        manager.GetComponentInChildren<UI_Handler>().gameObject.GetComponent<Canvas>().enabled = true;
        SceneManager.LoadScene(1);

    }

    //Show credits panel on press
    public void CreditsButtonPress()
    {
        Credits_panel.SetActive(true);
    }

    //Hide credits panel on press
    public void CloseCreditsPress()
    {
        Credits_panel.SetActive(false);
    }

    
}

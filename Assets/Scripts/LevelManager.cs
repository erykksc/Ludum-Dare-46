using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject [] levels;

    int activeLevel = 0;
    void Start()
    {
        int size = GameObject.FindGameObjectsWithTag("Level").Length;
        levels = new GameObject[size];
        for(int i = 0;i<levels.Length;i++)
        {
            levels[i] = GameObject.Find("Level"+i.ToString());
            levels[i].SetActive(false);
        }
        levels[0].SetActive(true);
        activeLevel = 0;
    }

    public void setNextLevel()
    {
        if(activeLevel>levels.Length-2)
        {return;}
        levels[activeLevel].SetActive(false);
        activeLevel++;
        levels[activeLevel].SetActive(true);
    }
    public void setPrevLevel()
    {
        if(activeLevel<1)
        {return;}
        levels[activeLevel].SetActive(false);
        activeLevel--;
        levels[activeLevel].SetActive(true);
    }
    public void setLevel(int index)
    {
        levels[activeLevel].SetActive(false);
        activeLevel = index;
        levels[activeLevel].SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            setNextLevel();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            setPrevLevel();
        }
    }
}

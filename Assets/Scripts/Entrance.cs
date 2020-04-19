using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LevelManager lManager;
    float age;
    void Start()
    {
        age = Time.time;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(Time.time-age<2)
        {
            return;
        }
        if(collider.gameObject.tag=="Player")
        {
            int count = Resources.FindObjectsOfTypeAll<LevelManager>().Length;
            if(count>0)
            {
                lManager = Resources.FindObjectsOfTypeAll<LevelManager>()[0];
            }
            Debug.Log("Success");
            if(lManager!=null)
            {
                if(SceneManager.GetActiveScene().buildIndex==1)
                {
                    return;
                }
                lManager.SwitchBack();
            }
        }
    }
   // void On
    // Update is called once per frame
    void Update()
    {
        
    }
}

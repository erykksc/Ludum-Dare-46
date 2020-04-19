using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Here loading screen can be added
//Level Manager
//Zarządza przejściami między poziomami
//Nic nie wymaga, jest zachowany pomiędzy scenami
//Player znajduje ten kompontent by zmienić poziom
public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    static private bool exists = false;
    void Awake()
    {
        if(exists)
        {
            Debug.Log("Destroying");
            Destroy(gameObject);
            return;
        }
        exists = true;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        
    }

    public void SwitchForth()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void SwitchBack()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex-1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

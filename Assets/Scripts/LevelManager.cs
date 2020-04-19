using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Here loading screen can be added
//Level Manager
// Co robi:
//Zarządza przejściami między poziomami
// Na czym powinien być:
//Wymaga image jako child, który jest loading screenem oraz dodanej referencji do jego image
//Player znajduje ten kompontent by zmienić poziom
public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    static private bool exists = false;
    [SerializeField] Image loadingScreen;
    [SerializeField] public int scene_shift=0;

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
        if(loadingScreen!=null)
        {
            loadingScreen.enabled = false;
        }
    }



    private Transform GetPlayerObject()
    {
        int count = Resources.FindObjectsOfTypeAll<Player>().Length;
        if (count > 0)
        {
            Player player = Resources.FindObjectsOfTypeAll<Player>()[0];
            Debug.Log("player found");
            return player.transform;
            
        }
        else return null;
    }

    private void SetPlayerPosition()
    {
        Transform warp_destination= null;
        //If going to previous level
        if (scene_shift ==-1)
        {
            Debug.Log("spawning at next");
            warp_destination = GameObject.FindGameObjectWithTag("Trigger_NEXT").transform; 
            GetPlayerObject().transform.position= 
                warp_destination.GetComponent<Rigidbody2D>().worldCenterOfMass;
            
        }

        //If going to next level
        else if (scene_shift ==1)
        {
            Debug.Log("spawning at previous");
            GameObject.FindGameObjectWithTag("Trigger_PREVIOUS");
            GetPlayerObject().transform.position = new Vector2(0, 0);
               // warp_destination.GetComponent<Rigidbody2D>().worldCenterOfMass;
           
        }
        Debug.Log("position set");
    }
    


    IEnumerator screenLoading(int index)
    {
        if(loadingScreen!=null)
        {
            loadingScreen.enabled = true;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
        if(loadingScreen!=null)
        {
            loadingScreen.enabled = false;
        }
        SetPlayerPosition();
        yield return null;
    }

    public void SwitchForth()
    {
        scene_shift=1;
        IEnumerator coroutine = screenLoading(SceneManager.GetActiveScene().buildIndex+1);
        StartCoroutine(coroutine);

    }
    public void SwitchBack()
    {
        scene_shift = -1;
        IEnumerator coroutine = screenLoading(SceneManager.GetActiveScene().buildIndex-1);
        StartCoroutine(coroutine);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

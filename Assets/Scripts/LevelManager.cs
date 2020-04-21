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
    [SerializeField] Image gameOverScreen;
    [SerializeField] AudioManager aManager;
    bool occupied = false;
    public static  bool reading_input = true;

    void Awake()
    {
        if (exists)
        {
            Debug.Log("Destroying");
            Destroy(gameObject);
            return;
        }
        exists = true;
        DontDestroyOnLoad(this);
        if (loadingScreen != null)
        {
            loadingScreen.enabled = false;
        }
        if (gameOverScreen != null)
        {
            gameOverScreen.enabled = false;
        }
        
        aManager = GetComponentInChildren<AudioManager>();
    }

    void SetPlayerPosition(Vector2 pos)
    {
        int count = Resources.FindObjectsOfTypeAll<Player>().Length;
        if(count>0)
        {
            Player player = Resources.FindObjectsOfTypeAll<Player>()[0];
            player.ClearState();
            player.transform.position = pos;
            count = Resources.FindObjectsOfTypeAll<UI_Handler>().Length;
            if(count>0)
            {
                UI_Handler handler = Resources.FindObjectsOfTypeAll<UI_Handler>()[0];
                handler.SetHealthBar(player.HP);
            }
        }
        count = Resources.FindObjectsOfTypeAll<Kid>().Length;
        if(count>0)
        {
            Kid kid = Resources.FindObjectsOfTypeAll<Kid>()[0];
            kid.transform.position = GetEntrancePos()+new Vector2(1,0);
            kid.CleanState();
        }
    }
    Vector2 GetEntrancePos()
    {
        int count = Resources.FindObjectsOfTypeAll<Entrance>().Length;
        if(count>0)
        {
            return Resources.FindObjectsOfTypeAll<Entrance>()[0].transform.position;
        }
        return new Vector2(0,0);
    }
    Vector2 GetExitPos()
    {
        int count = Resources.FindObjectsOfTypeAll<Exit>().Length;
        if(count>0)
        {
            return Resources.FindObjectsOfTypeAll<Exit>()[0].transform.position;
        }
        return new Vector2(0,0);
    }

    void setActivity(bool on)
    {
        GameObject[] objects = SceneManager.GetActiveScene().GetRootGameObjects();
        for(int i = 0;i<objects.Length;i++)
        {
            if(objects[i].tag=="MainCamera"){continue;}
            objects[i].SetActive(on);
        }
    }
    IEnumerator screenLoading(int i)
    {
        if(occupied)
        {
            yield break;
        }
        occupied = true;
        setActivity(false);
        if(gameOverScreen!=null&&i==0)
        {
            gameOverScreen.enabled = true;
            yield return new WaitUntil(() => Input.GetKey(KeyCode.F));
            gameOverScreen.enabled = false;
        }
        if(loadingScreen!=null)
        {
            loadingScreen.enabled = true;
        }
        aManager.StopTrack();

        int index = SceneManager.GetActiveScene().buildIndex+i;
        SceneManager.LoadScene(index);
        yield return new WaitForSeconds(0.5f);
        float t1 = Time.time;
        while(SceneManager.GetActiveScene().buildIndex!=index)
        {
            yield return new WaitForSeconds(0.05f);
            if(Time.time-t1>5f)
            {
                break;
            }
        }
        //Moving back
        if(i<0)
        {
            Debug.Log(GetExitPos());
            SetPlayerPosition(GetExitPos());
        }
        //Moving forth
        if(i>0)
        {
            Debug.Log(GetEntrancePos());
            SetPlayerPosition(GetEntrancePos());
        }
        //Restarting level
        if(i==0)
        {
            
            int count = Resources.FindObjectsOfTypeAll<Player>().Length;
            if(count>0)
            {
                Player player = Resources.FindObjectsOfTypeAll<Player>()[0];
                player.HP = player.maxHP;
            }
            SetPlayerPosition(GetEntrancePos());
        }


        if(loadingScreen!=null)
        {
            loadingScreen.enabled = false;
        }
        aManager.Stop();
        aManager.PlayTrack();
        occupied = false;
        //setActivity(true);
        yield return null;
    }

    void Update()
    {
        if (Input.GetKeyDown("r") && reading_input)
        {
            Restart();
        }
    }

    public void SwitchForth()
    {
        IEnumerator coroutine = screenLoading(1);
        StartCoroutine(coroutine);
    }
    public void SwitchBack()
    {
        IEnumerator coroutine = screenLoading(-1);
        StartCoroutine(coroutine);
    }
    public void Restart()
    {
        IEnumerator coroutine = screenLoading(0);
        StartCoroutine(coroutine);
    }
}



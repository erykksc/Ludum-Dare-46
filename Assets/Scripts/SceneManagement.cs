using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject [] persistables;
    [SerializeField] List<GameObject> levels;
    [SerializeField] int scenesCount = 0;
    List<Scene> scenes;

    static bool loaded = false;

    int currentScene = 0;
    void Start()
    {
        if(loaded){Destroy(this);return;}
        loaded = true;
        DontDestroyOnLoad(persistables[0]);
        DontDestroyOnLoad(this);
        scenes = new List<Scene>();
        levels = new List<GameObject>();
        for(int i = 0;i<scenesCount;i++)
        {
            if(i==SceneManager.GetActiveScene().buildIndex)
            {continue;}
            SceneManager.LoadSceneAsync(i,LoadSceneMode.Additive);
            GameObject[] goArray = (SceneManager.GetSceneByBuildIndex(i).GetRootGameObjects());
            Debug.Log(goArray.Length);
            Debug.Log("Level"+i.ToString());
            //levels.Add(SceneManager.GetSceneByBuildIndex(i))
        }
        for(int i = 0;i<scenesCount;i++)
        {
            //GameObject[] goArray = (SceneManager.GetSceneByBuildIndex(i).GetRootGameObjects());

            //Debug.Log(goArray.Length);
        }
        levels[2].SetActive(false);
    }
    void SwitchForth()
    {
        
    }
    void SwitchBack()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SwitchForth();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            SwitchBack();
        }
    }
}

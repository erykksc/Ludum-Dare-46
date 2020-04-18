using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject [] persistables;
    [SerializeField] List<Scene> levels;
    [SerializeField] int scenesCount = 0;
    List<Scene> scenes;

    static bool loaded = false;

    int currentScene = 0;
    void Start()
    {
        if(loaded){Destroy(this);return;}
        loaded = true;
        DontDestroyOnLoad(this);
        //DontDestroyOnLoad(persistables[0]);
        for(int i = 0;i<persistables.Length;i++)
        {
            Debug.Log(i);
            DontDestroyOnLoad(persistables[i]);
        }
        /*scenes = new List<Scene>();
        levels = new List<Scene>();
        for(int i = 0;i<scenesCount;i++)
        {
            if(i==SceneManager.GetActiveScene().buildIndex)
            {continue;}
            SceneManager.LoadSceneAsync(i,LoadSceneMode.Additive);
            Debug.Log("Level"+i.ToString());
            levels.Add(SceneManager.GetSceneByBuildIndex(i));
        }
        for(int i = 0;i<scenesCount;i++)
        {
            //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(i));
            GameObject[] goArray = (SceneManager.GetSceneByBuildIndex(i).GetRootGameObjects());

            Debug.Log(goArray.Length);
        }*/
    }
    void SwitchForth()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    void SwitchBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }


    IEnumerator screenLoading(int index)
    {
        if (loadingScreen != null)
        {
            loadingScreen.enabled = true;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
        if (loadingScreen != null)
        {
            loadingScreen.enabled = false;
        }

        yield return null;
    }

    public void SwitchForth()
    {
        IEnumerator coroutine = screenLoading(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(coroutine);

    }
    public void SwitchBack()
    {
        IEnumerator coroutine = screenLoading(SceneManager.GetActiveScene().buildIndex - 1);
        StartCoroutine(coroutine);

    }

   
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private string[] sounds2load;
    private Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

    void Start()
    {
        foreach(string clipName in sounds2load)
        {
            clips.Add(clipName, (AudioClip)Resources.Load(clipName));
        }
        
    }

    public AudioClip GetClip(string clipName){
        if(clips.ContainsKey(clipName)){
            return clips[clipName];
        }
        else
        {
            Debug.LogWarningFormat("Clipname {0} not found", clipName);
            return null;
        }
    }

    public AudioClip GetRandomClip(string[] clipNames)
    {
        //clips which have been loaded
        var loadedClips = new List<string>();

        foreach(string clipName in clipNames)
        {
            if(clips.ContainsKey(clipName))
            {
                loadedClips.Add(clipName);
            }
            else
            {
                Debug.LogWarningFormat("Clipname {0} not found", clipName);
            }
        }
        if(loadedClips.Count>0)
        {
            return clips[loadedClips[Random.Range(0, loadedClips.Count)]];
        }
        else
        {
            return null;
        }
    }

    public void PlayOneShot(string clipName, AudioSource source)
    {
        AudioClip clip = GetClip(clipName);
        if (clip is null)
        {
            Debug.LogAssertionFormat("Clipname {0} not found for source {1}", clipName, source.gameObject.name);
            return;
        }
        else
        {
            source.PlayOneShot(clip);
        }
    }
}

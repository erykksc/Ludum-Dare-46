using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Co robi:
// Ładuje i steruje odtwarzaniem audio

// Na czym powinien być:
// LevelManager w Menu

// Jakich komponentów wymaga: (np. Rigidbody2D)
// AudioSource na tym samym obiekcie gdzie ten skrypt jest dołączony

// Specjalne ustawienia objektu:
// sounds2load trzeba dodać stringi z nazwą wszystkich plików audio które powinny być w folderze Assets/Sound_Effects/Resources
// tracks - nazwy plików z soundtrackami do gier

public class AudioManager : MonoBehaviour
{
    //all sound files
    [SerializeField] private string[] sounds2load;
    //audio file names for soundtrack
    private Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
    [SerializeField] private string[] tracks;
    private AudioSource source;

    void Start()
    {
        //Tries to load all audio files
        foreach(string clipName in sounds2load)
        {
            clips.Add(clipName, (AudioClip)Resources.Load(clipName));
        }

        //Tries to find AudioSource
        source = GetComponent<AudioSource>();
        if (source is null)
        {
            Debug.LogError("AudioSource not added to gameobject: " + gameObject.name);
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

    public void PlayOneShot(string clipName)
    {
        AudioClip clip = GetClip(clipName);
        if (clip is null)
        {
            Debug.LogWarningFormat("Clipname {0} not found for source {1}", clipName, source.gameObject.name);
            return;
        }
        else
        {
            Debug.Log("Playing oneshot: " + clip.name);
            source.PlayOneShot(clip);
        }
    }

    public void PlayTrack()
    {
        //Plays rondom soundtrack
        source.clip = GetRandomClip(tracks);
        if(!(source.clip is null)){
            source.loop = true;
            Debug.Log("Clip name: " + source.clip.name);
            source.Play();
        }
    }

    public void Stop()
    {
        source.Stop();
    }

    public void StopTrack()
    {
        source.loop = false;
        source.Pause();
    }
}

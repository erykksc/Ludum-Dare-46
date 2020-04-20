using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip [] clips;
    [SerializeField] AudioSource source;

    int sIndex;
    void Start()
    {
        source = GetComponent<AudioSource>();
        sIndex = 1;
        setSong(sIndex);
    }

    public void Shuffle()
    {
        int index = Random.Range(1,clips.Length-1);
        if(index==sIndex)
        {
            index = (sIndex+1)%clips.Length;
        }
        if(index == 0){index++;}
        sIndex = index;
        source.clip = clips[sIndex];
        source.Play();
    }
    public void setSong(int index)
    {
        source.clip = clips[index%clips.Length];
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

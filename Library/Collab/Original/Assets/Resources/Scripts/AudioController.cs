using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClipEnum
{
    
}

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public List<AudioClip> audioClipList;
    public static AudioController instance;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClipEnum audioClipEnum)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClipList[(int) audioClipEnum]);
    }
}

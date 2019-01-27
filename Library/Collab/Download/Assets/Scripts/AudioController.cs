using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        GetComponent<AudioSource>().clip = audioClipList[Random.Range(0, 2) + 7];
        GetComponent<AudioSource>().Play();
    }

    public void PlayFingerClick()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClipList[0]);
    }
    
    public void PlayDistract()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClipList[Random.Range(0, 2) + 1]);
    }
    
    public void PlayGameOver()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClipList[Random.Range(0, 2) + 3]);
    }
    
    public void PlayWin()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClipList[Random.Range(0, 2) + 5]);
    }
    
    public void PlayBeingWatched()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClipList[Random.Range(0, 2) + 9]);
    }
    
    public void PlayVoice1()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClipList[10]);
    }
    
    public void PlayVoice2()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClipList[11]);
    }
}

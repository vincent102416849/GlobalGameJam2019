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

    public AudioSource bgmAS;
    public AudioSource clickAS;
    public AudioSource distractAS;
    public AudioSource gameOverAS;
    public AudioSource winAS;
    public AudioSource safeAS;
    public AudioSource voiceOverAS;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        bgmAS.clip = audioClipList[Random.Range(0, 2) + 7];
        bgmAS.Play();
    }

    public void Play(AudioClipEnum audioClipEnum)
    {
        bgmAS.PlayOneShot(audioClipList[(int) audioClipEnum]);
    }

    public void PlayClick()
    {
        clickAS.PlayOneShot(audioClipList[0]);
    }
    
    public void PlayDistract()
    {
        clickAS.PlayOneShot(audioClipList[Random.Range(0, 2) + 1]);
    }
    
    public void PlayGameOver()
    {
        clickAS.PlayOneShot(audioClipList[Random.Range(0, 2) + 3]);
    }
    
    public void PlayWin()
    {
        clickAS.PlayOneShot(audioClipList[Random.Range(0, 2) + 5]);
    }
    
    public void PlaySafe()
    {
        clickAS.PlayOneShot(audioClipList[9]);
    }
    
    public void PlayVoiceOver()
    {
        clickAS.PlayOneShot(audioClipList[Random.Range(0, 2) + 10]);
    }
}

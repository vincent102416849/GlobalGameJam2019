using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour
{
    public Animator fadePanelAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        EventManager.StartListening(GameEvent.Win.ToString(), OnWin);
        EventManager.StartListening(GameEvent.GameOver.ToString(), OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameEvent.Win.ToString(), OnWin);
        EventManager.StopListening(GameEvent.GameOver.ToString(), OnGameOver);
    }

    void OnWin()
    {
        Debug.Log("Win");
        AudioController.instance.PlayWin();
        StartCoroutine(StartOver("WinScene"));
    }

    void OnGameOver()
    {
        Debug.Log("Game Over");
        AudioController.instance.PlayGameOver();
        StartCoroutine(StartOver("LoseScene"));
    }

    IEnumerator StartOver(string targetSceneName)
    {
        fadePanelAnimator.SetBool("end", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(targetSceneName);
    }
}

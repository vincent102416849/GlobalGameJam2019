using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrans : MonoBehaviour {

    public Animator transitionAnim;
    public string sceneName1;


    public void OnOneClick()
    {
        StartCoroutine(LoadScence(sceneName1));
    }

    IEnumerator LoadScence(string sceneName)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class chooseAnimation : MonoBehaviour {

    public float duration = .5f;

    //public Vector3 des;
    //public Vector3 targetScale;

    public Button dad;
    public Button mom;
    public Button son;
    public Button daughter;
    public Button next;

    public float animatorSpeed = 1;

    public Animator transitionAnim;
    public string sceneName1;

    public ChosenChar cc;

    private bool dadClicked;
    private bool momClicked;
    private bool sonClicked;
    private bool daughterClicked;

    private Animator dadAni;
    private Animator momAni;
    private Animator sonAni;
    private Animator daughterAni;

    static int parent;
    static int kid;


    //public int frames;

    //private float fraction = 0;

    // Use this for initialization
    void Start () {
        dadClicked = false;
        momClicked = false;
        sonClicked = false;
        daughterClicked = false;

        dadAni = dad.GetComponent<Animator>();
        momAni = mom.GetComponent<Animator>();
        sonAni = son.GetComponent<Animator>();
        daughterAni = daughter.GetComponent<Animator>();
    }

    public void dadClick() {
        dadClicked = true;
        momClicked = false;
    }

    public void momClick()
    {
        momClicked = true;
        dadClicked = false;
    }

    public void sonClick()
    {
        sonClicked = true;
        daughterClicked = false;
    }

    public void daughterClick()
    {
        daughterClicked = true;
        sonClicked = false;
    }

    public void OnClick() {
        int count = 0;
        if(dadClicked || momClicked) {
            count++;
        }
        if(sonClicked || daughterClicked) {
            count++;
        }

        if (count == 2)
        {
            if (dadClicked)
            {
                dadAni.speed = animatorSpeed;
                dadAni.Play("dadMove", 0, 0f);
                momAni.speed = animatorSpeed;
                momAni.Play("FadeOut", 0, 0f);
                cc.setParent(1);
            }
            else if (momClicked)
            {
                momAni.speed = animatorSpeed;
                momAni.Play("momMove", 0, 0f);
                dadAni.speed = animatorSpeed;
                dadAni.Play("FadeOut", 0, 0f);
                cc.setParent(2);
            }

            if (sonClicked)
            {
                sonAni.speed = animatorSpeed;
                sonAni.Play("sonMove", 0, 0f);
                daughterAni.speed = animatorSpeed;
                daughterAni.Play("FadeOut", 0, 0f);
                cc.setKid(1);
            }
            else if (daughterClicked)
            {
                daughterAni.speed = animatorSpeed;
                daughterAni.Play("daughterMove", 0, 0f);
                sonAni.speed = animatorSpeed;
                sonAni.Play("FadeOut", 0, 0f);
                cc.setKid(2);
            }
            next.GetComponent<Button>().interactable = false;

            StartCoroutine(LoadScence(sceneName1));
        }


    }

    IEnumerator LoadScence(string sceneName)
    {
        yield return new WaitForSeconds(2f);
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }



    //private void changePosition(Button b) {
    //    Vector3 start = b.transform.position;
    //    for (int i = frames; i > 0; i --) {
    //        fraction += Time.deltaTime / duration;
    //        b.transform.position = Vector3.Lerp(start, des, fraction);
    //    }
    //}

    //private void changeSize(Button b) {
    //    Vector3 originalScale = b.transform.localScale;
    //    for (int i = frames; i > 0; i--) {
    //        fraction += Time.deltaTime / duration;
    //        b.transform.localScale = Vector3.Lerp(originalScale, targetScale, fraction);
    //    }
    //}
}

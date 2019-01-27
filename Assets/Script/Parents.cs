using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parents : MonoBehaviour {

    public GameObject parent;

    public Sprite dad;
    public Sprite mom;
    public Sprite dots;

    public void selectDad() {
        parent.GetComponent<SpriteRenderer>().sprite = dad;
    }

    public void selectMon() {
        parent.GetComponent<SpriteRenderer>().sprite = mom;
    }
}

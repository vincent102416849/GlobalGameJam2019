using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setUpChar : MonoBehaviour {

    public ChosenChar cc;
    public GameObject parent;
    public GameObject kid;

    public Sprite dad;
    public Sprite mom;
    public Sprite son;
    public Sprite daughter;

    private void Start()
    {
        return;
        if(cc.getKid() == 1) {
            kid.GetComponent<SpriteRenderer>().sprite = son;
        }
        else if (cc.getKid() == 2)
        {
            kid.GetComponent<SpriteRenderer>().sprite = daughter;
        }

        if (cc.getParent() == 1)
        {
            parent.GetComponent<SpriteRenderer>().sprite = dad;
        }
        else if (cc.getParent() == 2)
        {
            parent.GetComponent<SpriteRenderer>().sprite = mom;
        }

    }
}

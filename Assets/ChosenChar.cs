using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenChar : MonoBehaviour {

    static int parent;
    static int kid;

    public void setParent(int i) {
        parent = i;
    }

    public void setKid(int i) {
        kid = i;
    }

    public int getParent() {
        return parent;
    }

    public int getKid() {
        return kid;
    }

}

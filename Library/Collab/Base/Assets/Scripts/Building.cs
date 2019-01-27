using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float attractionForce;
    public float attractionProbability;
    public bool hasPassed;
    Target target;


    void Start()
    {
        target = FindObjectOfType<Target>();
    }

    void Update()
    {
        if (hasPassed)
            return;
        if (target.transform.position.y > transform.position.y) {
            if (Random.Range(0f, 1f) < attractionProbability) {
                target.attractionBuilding = this;
                target.beingAttracted = true;
            }
            hasPassed = true;
        }
    }
}

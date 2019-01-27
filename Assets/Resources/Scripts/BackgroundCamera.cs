using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCamera : MonoBehaviour
{
    [Range(0f, 1f)] public float moveSpeed;
    Target target;
    private Vector3 offset;

    void Start()
    {
        target = FindObjectOfType<Target>();
        offset = transform.position - target.transform.position;
    }

    void Update()
    {
        if (!target.beingAttracted)
        {
            var targetPosition = new Vector3(
                                     0.2f * target.transform.position.x,
                                     target.transform.position.y,
                                     target.transform.position.z
                                 )
                                 + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed);
        }
    }
}

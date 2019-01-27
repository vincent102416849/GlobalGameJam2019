using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    [Range(0f, 1f)] public float moveSpeed;
    public GameObject clickPS;
    public Vector3 moveAnchor;
    private bool hasTouched;

    void Start()
    {

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveAnchor, moveSpeed);

#if UNITY_EDITOR
        UpdateInputConsole();
#else
        UpdateInputMobile();
#endif
    }

    public void UpdateInputMobile() {
        if (Input.touchCount > 0)
        {
            if (hasTouched)
                return;
            hasTouched = true;
            ModifyMoveAnchor(Input.touches[0].position, camera);
        }
        else
            hasTouched = false;
    }

    public void UpdateInputConsole() {
        if (Input.GetMouseButtonDown(0))
            ModifyMoveAnchor(Input.mousePosition, camera);
    }

    void ModifyMoveAnchor(Vector2 _screenPosition, Camera cam)
    {
        AudioController.instance.PlayClick();
        var screenPosition = new Vector3(_screenPosition.x, _screenPosition.y, -camera.transform.position.z);
        var worldPosition = cam.ScreenToWorldPoint(screenPosition);
        Instantiate(clickPS).transform.position = worldPosition;
        moveAnchor = worldPosition;
    }
}

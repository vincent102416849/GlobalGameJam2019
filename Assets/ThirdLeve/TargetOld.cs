using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class TargetOld : MonoBehaviour
{
    [Range(0f, 10f)] public float moveSpeed;
    [Range(0f, 10f)] public float moveSpeedIncrement;
    [Range(0f, 1f)] public float lerpSpeed;
    [Range(0f, 2f)] public float monitorRange;
    public TargetState targetState;

    public ParticleSystem monitorPS;

    public float thresholdX;
    public float gameoverX;

    public bool beingAttracted;
    public Building attractionBuilding;
    public Player father;

    private Coroutine actionRoutine;

    void Start()
    {
        father = FindObjectOfType<Player>();
        var main = monitorPS.main;
        main.startSize = monitorRange * 1.5f;
        actionRoutine = StartCoroutine(StartWaitFatherLeave());
    }

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
        StopCoroutine(actionRoutine);
    }

    void OnGameOver()
    {
        StopCoroutine(actionRoutine);
    }

    bool IsFatherTooClose()
    {
        return Vector3.Distance(
                   Vector3.ProjectOnPlane(transform.position, Vector3.forward),
                   Vector3.ProjectOnPlane(father.transform.position, Vector3.forward)
               ) > monitorRange;
    }

    IEnumerator StartWaitFatherLeave()
    {
        targetState = TargetState.WaitFather;
        do
        {
            yield return null;
        } while (IsFatherTooClose());

        actionRoutine = StartCoroutine(StartWalkForKid());
    }
    IEnumerator StartWalkForKid() {
        targetState = TargetState.MoveForward;
        do
        {
            transform.position = Vector3.Lerp(
                transform.position,
                father.transform.position,
                lerpSpeed);
            if (Mathf.Abs(transform.position.y) > 20f)
                EventManager.TriggerEvent(GameEvent.Win.ToString());
            yield return null;
        } while (!IsFatherTooClose());

        if (IsFatherTooClose())
            actionRoutine = StartCoroutine(StartWaitFatherLeave());
    }

}

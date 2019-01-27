using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum TargetState
{
    WaitFather, MoveForward, Attracted, BackToCenter
}

public class Target : MonoBehaviour
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
               ) < monitorRange;
    }

    IEnumerator StartWaitFatherLeave()
    {
        targetState = TargetState.WaitFather;
        do
        {
            yield return null;
        } while (IsFatherTooClose());

        actionRoutine = StartCoroutine(StartWalkForward());
    }

    IEnumerator StartWalkForward()
    {
        targetState = TargetState.MoveForward;
        do
        {
            transform.position = Vector3.Lerp(
                transform.position,
                Vector3.Project(transform.position, Vector3.up) + Vector3.up * moveSpeed * Time.deltaTime,
                lerpSpeed);
            if (Mathf.Abs(transform.position.y) > 29f)
                EventManager.TriggerEvent(GameEvent.Win.ToString());
            yield return null;
        } while (!beingAttracted && !IsFatherTooClose());

        if (IsFatherTooClose())
            actionRoutine = StartCoroutine(StartWaitFatherLeave());
        else if (beingAttracted)
            actionRoutine = StartCoroutine(StartWalkToBuilding());
    }

    IEnumerator StartWalkToBuilding()
    {
        targetState = TargetState.Attracted;
        
        AudioController.instance.PlayDistract();
        
        var direction = (attractionBuilding.transform.position - transform.position).normalized;
        do
        {
            transform.position = Vector3.Lerp(
                transform.position,
                transform.position + direction * moveSpeed * Time.deltaTime,
                lerpSpeed);
            if (Mathf.Abs(transform.position.x) > gameoverX)
                EventManager.TriggerEvent(GameEvent.GameOver.ToString());
            yield return null;
        } while (!IsFatherTooClose());
        
        attractionBuilding = null;
        beingAttracted = false;
        
        actionRoutine = StartCoroutine(StartWalkBackToCenter());
    }

    IEnumerator StartWalkBackToCenter()
    {
        targetState = TargetState.BackToCenter;

        AudioController.instance.PlayBeingWatched();
        var targetAbsX = Mathf.Abs(transform.position.x);
        
        var tsa = monitorPS.textureSheetAnimation;
        tsa.cycleCount++;
        moveSpeed += moveSpeedIncrement;
        /*
        if (targetAbsX > thresholdX)
        {
            // Good job
            Debug.Log("Good job!");
        }
        else
        {
            // Oops...
            Debug.Log("Oops...");
            
        }
        */
        
        do
        {
            transform.position = Vector3.Lerp(
                transform.position,
                Vector3.Project(transform.position, Vector3.up),
                lerpSpeed * 0.1f);
            if (transform.position.x > gameoverX)
                EventManager.TriggerEvent(GameEvent.GameOver.ToString());
            yield return null;
        } while (Vector3.Distance(transform.position, Vector3.Project(transform.position, Vector3.up)) < 0.1f);

        transform.position = Vector3.Project(transform.position, Vector3.up);
        actionRoutine = StartCoroutine(StartWaitFatherLeave());
    }
}

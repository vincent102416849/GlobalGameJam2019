using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        AudioController.instance.PlayVoiceOver();
    }
}

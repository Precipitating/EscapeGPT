using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceDetected : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int hearingDistance = 15;
    public static event Action<string> onGuardHear;

    // same but without requiring the string parameter for ToggleMic class
    public event Action onGuardHearVoid;

    private void OnEnable()
    {
        ResultReceiver.onGuardHear += EnemyVoiceAction;
    }


    private void OnDisable()
    {
        ResultReceiver.onGuardHear -= EnemyVoiceAction;
    }

    // check if guard can "hear" from a specified distance
    void EnemyVoiceAction(string transcribeResult)
    {
        if (IsHearingRange())
        {
            onGuardHear?.Invoke(transcribeResult);
            onGuardHearVoid?.Invoke();
            Debug.Log("Enemy can hear this");

        }

        
    }

    public bool IsHearingRange()
    {
        return (Vector3.Distance(player.transform.position, transform.position) < hearingDistance);
    }
}

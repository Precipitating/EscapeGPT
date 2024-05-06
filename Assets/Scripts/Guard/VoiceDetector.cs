using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceDetected : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int hearingDistance = 10;
    public static event Action<string> OnGuardHear;

    private void OnEnable()
    {
        ResultReceiver.OnGuardHear += EnemyVoiceAction;
    }

    private void OnDisable()
    {
        ResultReceiver.OnGuardHear -= EnemyVoiceAction;
    }
    void EnemyVoiceAction(string transcribeResult)
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < hearingDistance)
        {
            OnGuardHear?.Invoke(transcribeResult);
            Debug.Log("Enemy can hear this");

        }

        
    }
}

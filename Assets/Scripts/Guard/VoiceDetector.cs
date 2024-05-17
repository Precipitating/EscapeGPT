using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceDetected : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int hearingDistance = 15;
    public static event Action<string> onGuardHear;

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
        if (Vector3.Distance(player.transform.position, this.transform.position) < hearingDistance)
        {
            onGuardHear?.Invoke(transcribeResult);
            Debug.Log("Enemy can hear this");

        }

        
    }
}

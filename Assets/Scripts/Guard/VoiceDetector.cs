using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceDetected : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int hearingDistance = 10;
    public static event Action OnEnemyHear;

    private void OnEnable()
    {
        ResultReceiver.OnGuardHear += EnemyVoiceAction;
    }

    private void OnDisable()
    {
        ResultReceiver.OnGuardHear -= EnemyVoiceAction;
    }
    void EnemyVoiceAction()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < hearingDistance)
        {
            OnEnemyHear?.Invoke();
            Debug.Log("Enemy can hear this");

        }

        
    }
}

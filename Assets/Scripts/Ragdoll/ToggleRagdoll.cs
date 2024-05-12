using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ToggleRagdoll : MonoBehaviour
{
    // holds all ragdoll rigid bodies
    [SerializeField] private Rigidbody[] ragdollRb;
    [SerializeField] private MonoBehaviour[] allScripts;
    private bool isEnabled = false;
    bool hasNavMeshAgent = false;

    private Animator animator;
    private NavMeshAgent agent;


     void Awake()
    {
        ragdollRb = GetComponentsInChildren<Rigidbody>();
        allScripts = GetComponents<MonoBehaviour>();
        animator = GetComponent<Animator>();
        hasNavMeshAgent = TryGetComponent(out agent);
    }




    public void Toggle()
    {
        isEnabled = !isEnabled;
        // turn on/off ragdoll
        foreach (var rb in ragdollRb)
        {
            rb.isKinematic = isEnabled;
        }

        // turn on/off relevant components
        foreach (MonoBehaviour component in allScripts)
        {
            component.enabled = isEnabled;
        }
        animator.enabled = isEnabled;

        if (hasNavMeshAgent)
        {
            agent.enabled = isEnabled;
        }
        



    }


}

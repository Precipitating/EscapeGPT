using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public enum State
    {
        IDLE,
        PATROL,
        ATTACKING
    }
    
    // get refs 
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private Transform characterPosition;
    [SerializeField] private Transform leverPosition;
    [SerializeField] private int angerValue = 0;
    [SerializeField] private const int maxAnger = 3;
    [SerializeField] private MeshRenderer sheathedSword;
    [SerializeField] private MeshRenderer unsheathedSword;


    State guardState = State.IDLE;
    int patrolDest = 0;

    private void OnEnable()
    {
        ChatGPTReceiver.onChatGPTResult += Converse;
    }

    private void OnDisable()
    {
        ChatGPTReceiver.onChatGPTResult -= Converse;
    }



    private void OnValidate()
    {
        if (!agent) agent.GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponent<Animator>();

    }


    void Update()
    {
        if (agent.hasPath)
        {
            animator.SetFloat("Y Velocity", agent.velocity.magnitude);

        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }

        switch (guardState)
        {
            case State.IDLE: Setup(); break;
            case State.PATROL: Patrol(); break;
            case State.ATTACKING: Attack(); break;
        }
    }

    private void Setup()
    {
        guardState = State.PATROL;
        agent.SetDestination(waypoints[patrolDest].transform.position);
    }

    private void Patrol()
    {
        if (!agent.hasPath || agent.remainingDistance < 0.1f)
        {
            patrolDest = patrolDest == 0 ? 1 : 0;
            agent.SetDestination(waypoints[patrolDest].transform.position);
        }
    }

    private void Converse(string chatGPTResult)
    {

        if (chatGPTResult == "1")
        {
            if (angerValue >= maxAnger)
            {
                ChangeState(State.ATTACKING);
                agent.speed *= 2;
                
            }
            else
            {
                ++angerValue;
            }
        }
        else
        {
            // pay no mind and play audio
        }


    }
    private void Attack()
    {
        // check if player is accessible 
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(characterPosition.position, path);
        if (path.status == NavMeshPathStatus.PathPartial || path.status ==  NavMeshPathStatus.PathInvalid)
        {
            // open lever to access player
            agent.destination = leverPosition.position;

        }
        else
        {
            // path valid, ATTACK
            agent.destination = characterPosition.position;

            // if close, play sword swing
        }

        
    
    }

    public void ChangeState(State state)
    {
        guardState = state;
    }

    public State GetState()
    {
        return guardState;
    }
    public void SetStoppingDistance(int distance)
    {
        agent.stoppingDistance = distance;
    }




}

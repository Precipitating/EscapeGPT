using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    enum State
    {
        IDLE,
        PATROL,
        ATTACKING
    }
    
    // get refs 
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] GameObject[] waypoints;
    [SerializeField] Transform characterPosition;


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
        if (agent.hasPath)
        {
            animator.SetFloat("Y Velocity", agent.velocity.magnitude);

        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            patrolDest = patrolDest == 0 ? 1 : 0;
            agent.SetDestination(waypoints[patrolDest].transform.position);

        }
    }

    private void Converse(string chatGPTResult)
    {
        agent.isStopped = true;
        transform.LookAt(characterPosition);

        // do work
        Debug.Log(chatGPTResult);

        agent.isStopped = false;



    }
    private void Attack()
    {


    
    }

    private void ChangeState(State state)
    {
        guardState = state;
    }




}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour, HumanInterface
{
    public enum State
    {
        IDLE,
        PATROL,
        ATTACKING,
        DEAD
    }

    // get refs 
    [SerializeField] private int hp = 100;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] waypoints;

    // transforms for guard interest points (navigation)
    [SerializeField] private Transform characterPosition;
    [SerializeField] private Transform leverPosition;

    // anger value determined by player speech
    [SerializeField] private int angerValue = 0;
    [SerializeField] private const int maxAnger = 3;

    // sword
    [SerializeField] private MeshRenderer sheathedSword;
    [SerializeField] private MeshRenderer unsheathedSword;

    // attack
    [SerializeField] private int attackAnimations;
    [SerializeField] private int attackDistance = 1;
    [SerializeField] private bool isAttacking = false;

    private bool canDamage = false;

    // controls guard state
    State guardState = State.IDLE;

    // for patrolling, the number represents patrol waypoints indexes in a list
    int patrolDest = 0;

    private void OnEnable()
    {
        // grab the guard's response
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

        // apply correct blend tree animation for guard walk/run (speed dependent)
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
            case State.DEAD: Die(); break;
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
            // keep switching back and forth waypoints which there is currently 2 waypoints
            patrolDest = patrolDest == 0 ? 1 : 0;
            agent.SetDestination(waypoints[patrolDest].transform.position);
        }
    }

    private void Converse(string chatGPTResult)
    {
        // if guard is already attacking, no point talking to you.
        if (guardState != State.ATTACKING)
        {
            // 1 = ChatGPT deems what you said insulting
            if (chatGPTResult == "1")
            {
                // guard wants you dead, set to attack state
                if (angerValue >= maxAnger)
                {
                    ChangeState(State.ATTACKING);
                    agent.speed *= 2;

                    // unsheath weapon
                    animator.Play("Unsheath");
                    unsheathedSword.enabled = true;
                    sheathedSword.enabled = false;

                }
                else
                {
                    ++angerValue;
                }
            }
            else
            {
                // pay no mind and play disregarding audio
            }
        }



    }
    private void Attack()
    {
        // check if guard -> player is reachable
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(characterPosition.position, path);
        if (path.status == NavMeshPathStatus.PathPartial || path.status ==  NavMeshPathStatus.PathInvalid)
        {
            // if not, open lever so player is reachable
            agent.destination = leverPosition.position;

        }
        else
        {
            isAttacking = animator.GetBool("isAttacking");
            // follow player until near stopping distance
            if (!isAttacking && !ReachedDestination())
            {
                agent.destination = characterPosition.position;
            }
            else
            {

                // path valid, rush player and attack when near player
                if (ReachedDestination())
                {
                    transform.LookAt(characterPosition.position);

                    if (!isAttacking)
                    {
                        // stay in place before attacking
                        agent.SetDestination(transform.position);

                        // pick a random attack and execute it
                        int randomAttackAnim = UnityEngine.Random.Range(1, attackAnimations + 1);
                        animator.SetInteger("AttackType", randomAttackAnim );
                        animator.SetBool("isAttacking", true);




                    }

                }
            }



        }

        
    
    }

    // change guard state
    public void ChangeState(State state)
    {
        guardState = state;
    }

    public State GetState()
    {
        return guardState;
    }
    // --
    public void SetStoppingDistance(int distance)
    {
        agent.stoppingDistance = distance;
    }

    // distance between player and enemy before attacking
    public int GetAttackDistance()
    {
        return attackDistance;
    }

    // check if attack animation is playing via animation events
    public void SetAnimPlaying(int set)
    {
        if (set == 0)
        {
            animator.SetBool("isAttacking", false);
            animator.SetInteger("AttackType", 0);
            agent.destination = characterPosition.position;
        }


    }


    // check if navmesh has reached destination
    public bool ReachedDestination()
    {
        bool result = false;
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    result =  true;
                }
            }
        }

        return result;
    }

    // should activate and deactivate within certain frames of the sword swing animation
    public void EnableSwordDamage(int enabled)
    {
        if (enabled == 1)
        {
            canDamage = true;
        }
        else
        {
            canDamage = false;
        }

    }

    // returns if sword can damage the player
    public bool CanDamage()
    {
        return canDamage;
    }

    public int HP
    {
        get { return hp; } 
        set { hp = value; }
    }

    public void OnHit(int dmg)
    {
        HP -= dmg;

        if (HP <= 0)
        {
            guardState = State.DEAD;
        }



    }

    public void Die()
    {
        ToggleRagdoll script;

        if (TryGetComponent(out script))
        {
            script.Toggle();
        }
        else
        {
            Debug.Log("Guard does not have ToggleRagdoll script added in!");
        }
    }






}

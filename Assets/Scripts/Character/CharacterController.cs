using PrimeTween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour, HumanInterface
{

    // -- references
    [SerializeField] int hp = 100;
    [SerializeField] private float animBlendSpeed = 8.9f;

    // camera 
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float camUpperLimit = -40f;
    [SerializeField] private float camBottomLimit = 70f;
    
    // mouse
    [SerializeField] private float mouseSens = 21.9f;

    // voice -> text transcription
    [SerializeField] private VoskSpeechToText vosk;
    [SerializeField] private VoiceProcessor voiceProcessor;

    // camera animation
    [SerializeField] private float cameraShakeIntensity = 1f;
    [SerializeField] private float cameraShakeDuration = 0.5f;

    // can damage enemies
    private bool canDamage = false;

    public static event Action onPlayerDead;
    [SerializeField] private float deathAnimationTime = 2f;




    private Rigidbody playerRigBody;
    private InputManager inputManager;
    private Animator animator;

    // animation
    private bool hasAnimator;
    private int xVelHash;
    private int yVelHash;

    // speed
    [SerializeField] private const float  walkSpeed = 2f;
    [SerializeField] private const float runSpeed = 6f;
    private Vector2 currentVel;

    // camera
    private float xRotation;



    private void Start()
    {
        hasAnimator = TryGetComponent(out animator);
        playerRigBody = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();

        xVelHash = Animator.StringToHash("X Velocity");
        yVelHash = Animator.StringToHash("Y Velocity");

    }

    // force based movement
    private void Move()
    {
        if (!hasAnimator) return;

        float targetSpeed = inputManager.Run ? runSpeed : walkSpeed;

        if (inputManager.Move == Vector2.zero)
        {
            targetSpeed = 0.1f;
        }

        currentVel.x = Mathf.Lerp(currentVel.x, inputManager.Move.x * targetSpeed, animBlendSpeed * Time.fixedDeltaTime);
        currentVel.y = Mathf.Lerp(currentVel.y, inputManager.Move.y * targetSpeed, animBlendSpeed * Time.fixedDeltaTime);
        

        var xVelDifference = currentVel.x - playerRigBody.velocity.x; 
        var zVelDifference = currentVel.y - playerRigBody.velocity.z; 

        playerRigBody.AddForce(transform.TransformVector(new Vector3(currentVel.x, 0, currentVel.y)), ForceMode.VelocityChange);

        animator.SetFloat(xVelHash, currentVel.x);
        animator.SetFloat(yVelHash, currentVel.y);

    }

    // camera movement with the y axis clamped
    private void CameraMove()
    {
        if (!hasAnimator) return;
        var mouseX = inputManager.Look.x;
        var mouseY = inputManager.Look.y;
        cameraTransform.position = cameraRoot.position;

        xRotation -= mouseY * mouseSens * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, camUpperLimit, camBottomLimit);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up, mouseX * mouseSens * Time.deltaTime);





    }

    private void Update()
    {
        // handle mic toggling
        if (inputManager.ToggleMic)
        {
            if (!voiceProcessor.IsRecording)
            {
                vosk.ToggleRecording();
            }
        }







    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraMove();
    }


    // on player hit, shake camera and lower HP
    public void OnHit(int dmg)
    {
        Tween.ShakeLocalPosition(cameraRoot, new Vector3(cameraShakeIntensity, cameraShakeIntensity, cameraShakeIntensity), cameraShakeDuration);

        HP -= dmg;

        if (HP <= 0)
        {
            Die();
        }



    }

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    
    }  

    public void Die()
    {
        // play death animation, animation event handles death screen.
        animator.Play("Death");
        


    }

    // once death animation is finished, this will be invoked via animation event.
    public void InvokeDeathEvent()
    {
        onPlayerDead?.Invoke();
    }

    // can player deal any damage? refer below function
    public bool CanDamage()
    {
        return canDamage;
    }


    // this will activate automatically when the player plays an attack animation via animation event
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















}

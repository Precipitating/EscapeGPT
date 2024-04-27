using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class CharacterController : MonoBehaviour
{

    // references
    [SerializeField] private float animBlendSpeed = 8.9f;

    [SerializeField] private Transform cameraRoot;
    [SerializeField] private Transform camera;
    [SerializeField] private float camUpperLimit = -40f;
    [SerializeField] private float camBottomLimit = 70f;
    [SerializeField] private float mouseSens = 21.9f;


    private Rigidbody playerRigBody;
    private InputManager inputManager;
    private Animator animator;

    // animation
    private bool hasAnimator;
    private int xVelHash;
    private int yVelHash;

    // speed
    private const float  walkSpeed = 2f;
    private const float runSpeed = 6f;
    private Vector2 currentVel;

    // camera
    private float xRotation;



    private void Start()
    {
        hasAnimator = TryGetComponent<Animator>(out animator);
        playerRigBody = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();

        xVelHash = Animator.StringToHash("X Velocity");
        yVelHash = Animator.StringToHash("Y Velocity");

    }

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

    private void CameraMove()
    {
        if (!hasAnimator) return;
        var mouseX = inputManager.Look.x;
        var mouseY = inputManager.Look.y;
        camera.position = cameraRoot.position;

        xRotation -= mouseY * mouseSens * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, camUpperLimit, camBottomLimit);

        camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up, mouseX * mouseSens * Time.deltaTime);





    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraMove();
    }










}

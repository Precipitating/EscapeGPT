using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Run { get; private set; }

    public bool Attack{ get; private set; }



    private InputActionMap currentMap;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction runAction;


    private InputAction attack;



    private void OnEnable()
    {
        currentMap.Enable();
        CharacterController.onPlayerDead += ShowCursor;
        ExitDoor.OnEscape += ShowCursor;  
    }


    private void OnDisable()
    {
        currentMap.Disable();
        CharacterController.onPlayerDead -= ShowCursor;
        ExitDoor.OnEscape -= ShowCursor;

    }

    private void Awake()
    {
        HideCursor();
        currentMap = playerInput.currentActionMap;
        moveAction = currentMap.FindAction("Move");
        lookAction = currentMap.FindAction("Look");
        runAction = currentMap.FindAction("Run");
        attack = currentMap.FindAction("Attack");


        moveAction.performed += onMove;
        lookAction.performed += onLook;
        runAction.performed += onRun;
        attack.performed += onAttack;

        moveAction.canceled += onMove;
        lookAction.canceled += onLook;
        runAction.canceled += onRun;
        attack.canceled += onAttack;


    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void onMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }
    private void onLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }
    private void onRun(InputAction.CallbackContext context)
    {
        Run = context.ReadValueAsButton();
    }

    private void onAttack(InputAction.CallbackContext context)
    {
        Attack = context.ReadValueAsButton();

    }  





}

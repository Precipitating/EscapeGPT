using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Run { get; private set; }

    public bool ToggleMic { get; private set; }
    public bool ToggleInteract { get; private set; }



    private InputActionMap currentMap;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction runAction;
    private InputAction toggleMicAction;
    private InputAction toggleInteract;

    private void OnEnable()
    {
        currentMap.Enable();
        CharacterController.onPlayerDead += ShowCursor;
    }


    private void OnDisable()
    {
        currentMap.Disable();
        CharacterController.onPlayerDead -= ShowCursor;

    }

    private void Awake()
    {
        HideCursor();
        currentMap = playerInput.currentActionMap;
        moveAction = currentMap.FindAction("Move");
        lookAction = currentMap.FindAction("Look");
        runAction = currentMap.FindAction("Run");
        toggleMicAction = currentMap.FindAction("ToggleMic");
        toggleInteract = currentMap.FindAction("Interact");


        moveAction.performed += onMove;
        lookAction.performed += onLook;
        runAction.performed += onRun;
        toggleMicAction.performed += onToggleMic;
        toggleInteract.performed += onInteract;

        moveAction.canceled += onMove;
        lookAction.canceled += onLook;
        runAction.canceled += onRun;
        toggleMicAction.canceled += onToggleMic;
        toggleInteract.canceled += onInteract;


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
    private void onToggleMic(InputAction.CallbackContext context)
    {
        ToggleMic = context.ReadValueAsButton();
    }

    private void onInteract(InputAction.CallbackContext context)
    {
        ToggleInteract = context.ReadValueAsButton();

    }



}

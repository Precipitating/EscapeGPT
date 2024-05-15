using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    [SerializeField] private float interactionDistance = 1f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private InputManager inputManager;

    [SerializeField] private Camera camera;


    public void Update()
    {
       
        if (inputManager.ToggleInteract)
        {
            ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            // only detects objects on layer Interactable at a distance of 1
            if (Physics.Raycast(ray, out hit, interactionDistance, interactableMask))
            {
                Debug.Log("Interactable detected!");
                // call the interact function on the object which will either pickup or open/close depending on object type.
                hit.transform.gameObject.GetComponent<InteractableInterface>().Interact();
            }


        }

    }
}

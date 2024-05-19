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

    private Outline currentObjectOutline;

    private bool isOn = false;
    public void Update()
    {
        ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        // only detects objects on layer Interactable at a distance of interactionDistance
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableMask))
        {

            // show outline if hovered over
            if (!isOn)
            {
                if (currentObjectOutline == null )
                {
                    currentObjectOutline = hit.transform.gameObject.GetComponent<Outline>();

                }

                currentObjectOutline.enabled = true;
                isOn = true;
            }


            // handle interaction
            if (inputManager.ToggleInteract)
            {
                // call the interact function on the object which will either pickup or open/close depending on object type.
                hit.transform.gameObject.GetComponent<InteractableInterface>().Interact();
            }

        }
        else
        {
            if (isOn)
            {
                currentObjectOutline.enabled = false;
                currentObjectOutline = null; 
                isOn = false;
            }
        }




    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenClosables : MonoBehaviour, InteractableInterface
{
    public UnityEvent openCloseObject;
    public void Interact()
    {
        // we don't know exactly what the object is, so we can assign the specific function in inspector.
        openCloseObject.Invoke(); 

    }

}

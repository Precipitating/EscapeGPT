using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour, InteractableInterface
{

    public void Interact()
    {
        // store in player inventory


        // then set inactive
        gameObject.SetActive(false);


    }


    
}

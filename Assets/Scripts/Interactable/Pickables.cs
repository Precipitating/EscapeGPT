using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour, InteractableInterface
{
    public void Interact()
    {
        Item currentItem = new Item(gameObject.tag);
        // store in player inventory
        Inventory.instance.AddItem(currentItem);

        // play pick up sound
        AudioManager.instance.PlayGlobalSFX("Pickup");


        // then set inactive
        gameObject.SetActive(false);


    }


    
}

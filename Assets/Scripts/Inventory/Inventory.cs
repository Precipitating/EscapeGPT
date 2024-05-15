using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField] private List<Item> inventory = new List<Item>();


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void AddItem(Item itemToAdd)
    {
        if (inventory.Contains(itemToAdd))
        {
            return;
        }
        inventory.Add(itemToAdd);
        Debug.Log(itemToAdd.name + " added");


    } 
    
    public void RemoveItem(Item itemToRemove)
    {
        if (inventory.Contains(itemToRemove))
        {
            inventory.Remove(itemToRemove);
            Debug.Log("Removed " + itemToRemove.name);
        }


    }


    public bool Exists(string itemTag) 
    {

        return inventory.Any(item => item.name == itemTag);
    }




}

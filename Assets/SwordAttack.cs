using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private MeshRenderer renderedMesh;
    [SerializeField] private Guard guardScript;
    public static event Action onPlayerHit;


    private void OnTriggerEnter(Collider other)
    {
        // if mesh is rendered, it means it is active and should work
        if (renderedMesh.enabled)
        {

            if (other.tag == "Player" && guardScript.CanDamage() )
            {
                Debug.Log("Hit!");
                onPlayerHit.Invoke();
            }
        }
    }



}

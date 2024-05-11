using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverOpen : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private bool isOpen = false;
    [SerializeField] private Animator doorAnimator = null;
    [SerializeField] private Guard guardScript;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            if (!isOpen && guardScript.GetState() == Guard.State.ATTACKING)
            {
                doorAnimator.Play("GateOpen");
                isOpen = true;
                guardScript.SetStoppingDistance(guardScript.GetAttackDistance());
            }
        }
        
    }


}

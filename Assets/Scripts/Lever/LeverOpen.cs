using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeverOpen : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Animator doorAnimator = null;
    [SerializeField] private Guard guardScript;

    

    // for guard only
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            if (!doorAnimator.GetBool("isOpen") && guardScript.GetState() == Guard.State.ATTACKING)
            {
                doorAnimator.Play("GateOpen");
                doorAnimator.SetBool("isOpen",true);
                guardScript.SetStoppingDistance(guardScript.GetAttackDistance());
            }
        }
        
    }



    public void openOrClose()
    {

        if (!doorAnimator.IsInTransition(0))
        {
            if (doorAnimator.GetBool("isOpen"))
            {
                doorAnimator.SetBool("isOpen", false);

            }
            else
            {
                doorAnimator.SetBool("isOpen", true);
            }
        }








    }


}

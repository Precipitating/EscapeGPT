using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCombat : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Equip equipScript;
    [SerializeField] private int attackVariations = 2;


    private bool isReady = true;

    private bool canParry = true;


    void Update()
    {
        // attack
        if (Mouse.current.leftButton.wasPressedThisFrame && equipScript.IsHoldingSword())
        {
            if (IsReady())
            {   
                playerAnimator.SetInteger("SwordAttack", Random.Range(0, attackVariations + 1));
            }
        }
        // deflect
        else if (Mouse.current.rightButton.wasPressedThisFrame && 
                 equipScript.IsHoldingSword() &&
                 IsReady() && canParry)
        {


            canParry = false;
            playerAnimator.SetTrigger("isDeflecting");
        }

    }



    // true = not playing a sword slash animation
    // false = is in the midst of animating a sword slash
    private bool IsReady()
    {
        return isReady;
    }


    public void SetAnimatingAttack(int val)
    {
        switch (val)
        {
            case 1: isReady = false; break;
            case 2: isReady = true; playerAnimator.SetInteger("SwordAttack", 0); break;
        
        }
    }


    public void ResetParry()
    {
        canParry = true;
    }


}

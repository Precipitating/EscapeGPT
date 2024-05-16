using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Equip equipScript;
    [SerializeField] private int attackVariations = 2;

    bool isReady = true;

    void Update()
    {
        if (inputManager.Attack && equipScript.IsHoldingSword())
        {
            if (IsAnimatingAttack())
            {
                playerAnimator.SetInteger("SwordAttack", Random.Range(0, attackVariations + 1));
            }
        }
    }



    private bool IsAnimatingAttack()
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
}

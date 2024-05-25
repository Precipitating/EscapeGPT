using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equip : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private MeshRenderer targetObject;
    
    int nextAnimToPlay = 1;


    private bool swordDrawn = false;



    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
        {
            Debug.Log("Attempt to equip sword!");
            if (Inventory.instance.Exists("Sword"))
            {
                bool isAlreadyPlaying = playerAnimator.GetBool("isPlaying");

                if (!isAlreadyPlaying)
                {
                    playerAnimator.SetInteger("isDrawn", nextAnimToPlay);

                    switch (nextAnimToPlay)
                    {
                        case 1: AudioManager.instance.PlayGlobalSFX("Unsheath"); break;
                        case 2: AudioManager.instance.PlayGlobalSFX("Sheath"); break;
                    }


                }
            }

        }
    }




    // animation event hands showing weapon visibility unsheath/sheath
    public void WeaponVisible(int val)
    {
        switch (val)
        {
            case 1: targetObject.enabled = true; break;
            case 2: targetObject.enabled = false; break;

        }

    }


    public void IsPlaying(int val)
    {
        switch (val)
        {
            case 1: playerAnimator.SetBool("isPlaying", true); break;
            case 2:
                {
                    playerAnimator.SetBool("isPlaying", false);

                    switch (playerAnimator.GetInteger("isDrawn"))
                    {
                        case 1: nextAnimToPlay = 2; swordDrawn = true; break;
                        case 2: nextAnimToPlay = 1; swordDrawn = false; break;

                    };
                    playerAnimator.SetInteger("isDrawn", 0);
                    break;
                }


        }

        

    }


    public bool IsHoldingSword()
    {
        return swordDrawn;
    }



}

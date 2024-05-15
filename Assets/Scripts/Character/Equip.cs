using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equip : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject targetObject;
    int nextAnimToPlay = 1;



    // Update is called once per frame
    void Update()
    {
        if (inputManager.ToggleSlot1)
        {
            Debug.Log("Attempt to equip sword!");
            if (Inventory.instance.Exists("Sword"))
            {
                bool isAlreadyPlaying = playerAnimator.GetBool("isPlaying");

                if (!isAlreadyPlaying)
                {
                    playerAnimator.SetInteger("isDrawn", nextAnimToPlay);



                }
            }

        }
    }




    // animation event hands showing weapon visibility unsheath/sheath
    public void WeaponVisible(int val)
    {
        switch (val)
        {
            case 1: targetObject.SetActive(true); break;
            case 2: targetObject.SetActive(false); break;

        }

    }


    public void IsPlaying(int val)
    {
        switch (val)
        {
            case 1:
                playerAnimator.SetBool("isEquipPlaying", true);

                break;

            case 2:
                {
                    playerAnimator.SetBool("isEquipPlaying", false);

                    switch (playerAnimator.GetInteger("isDrawn"))
                    {
                        case 1: nextAnimToPlay = 2; break;
                        case 2: nextAnimToPlay = 1; break;

                    };

                    playerAnimator.SetInteger("isDrawn", 0);

                    break;
                }


        }

    }
}

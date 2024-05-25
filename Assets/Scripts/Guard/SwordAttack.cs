using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private MeshRenderer renderedMesh;
    [SerializeField] private int dmg = 20;
    [SerializeField] HumanInterface attachedController;
    [SerializeField] ParticleSystem parryEffect;

    private void Awake()
    {
        renderedMesh = GetComponent<MeshRenderer>();
        attachedController = GetComponentInParent<HumanInterface>();

    }

    private void OnTriggerEnter(Collider other)
    {
        // if mesh is rendered, it means it is active and should work
        if (renderedMesh.enabled)
        {
            // make sure it cant hurt itself 
            if (other.tag != "Untagged" && other.tag != transform.root.tag )
            {
                // this should get the script that controls an enemy or player
                HumanInterface unknownController =  other.GetComponentInParent<HumanInterface>();

                if (attachedController.CanDamage())
                {
                    // parried
                    if (unknownController.CanParry)
                    {
                        Debug.Log(other.tag + "Deflected!");
                        AudioManager.instance.PlayRandom2D(AudioManager.instance.swordClashList);
                        parryEffect.Play();
                    }
                    else
                    {
                        Debug.Log(other.tag + "Hit!");
                        unknownController.OnHit(dmg);

                        // play a random flesh cut sound effect
                        AudioManager.instance.PlayRandom2D(AudioManager.instance.fleshCutList);
                    }



                    // 2 = false, animation event does not accept bool
                    // disable sword dmg so it doesn't retrigger multiple times per swing
                    attachedController.EnableSwordDamage(2);
                }
            }


        }
    }



}

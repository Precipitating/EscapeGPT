using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootstep : MonoBehaviour
{

    [SerializeField] AudioClip[] footStepSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] HumanInterface human;

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.Log("Assign an Audio Source to your object for this script (Footstep) to work!");
        }
        else
        {
            human = GetComponent<HumanInterface>();
        }
    }

    public void Footstep(AnimationEvent val)
    {
        if (val.animatorClipInfo.weight > 0.5f)
        {
            audioSource.PlayOneShot(footStepSound[UnityEngine.Random.Range(0, footStepSound.Length)]);
        }

        

        
    }
}

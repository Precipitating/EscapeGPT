using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootstep : MonoBehaviour
{

    [SerializeField] AudioClip[] footStepSound;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.Log("Assign an Audio Source to your object for this script (Footstep) to work!");
        }
        else
        {
            audioSource = GetComponent<AudioSource>(); 
        }
    }

    public void Footstep()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(footStepSound[UnityEngine.Random.Range(0, footStepSound.Length)]);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// should be handled via animation events
public class PlayRandom: MonoBehaviour
{

    [SerializeField] private AudioClip[] randomFootstep;
    [SerializeField] private AudioSource audioSource;

    public UnityEvent invokeSwordSound;



    public void Footstep(AnimationEvent val)
    {
        if (val.animatorClipInfo.weight > 0.5f)
        {
            audioSource.PlayOneShot(randomFootstep[UnityEngine.Random.Range(0, randomFootstep.Length)]);
        }

    }

    // using different audio source and is in a different location.
    // Animation events only work if animator is in same location, so trigger it via UnityEvents.
    public void Sword()
    {
        invokeSwordSound?.Invoke();
        


    }




}

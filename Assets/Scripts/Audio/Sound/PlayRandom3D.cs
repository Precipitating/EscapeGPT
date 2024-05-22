using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// 3D sound mostly be handled via animation events
public class PlayRandom3D: MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource swordAudioSource;

    public void Sword()
    {
        swordAudioSource.PlayOneShot(AudioManager.instance.GetRandomClip(AudioManager.instance.swordMissList));

        

    }

    public void Unsheath()
    {
        audioSource.PlayOneShot(AudioManager.instance.GetGlobalSFX("Unsheath"));
    }




}

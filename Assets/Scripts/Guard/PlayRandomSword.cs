using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSword : MonoBehaviour
{
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    public void Sword()
    {
        audioSource.PlayOneShot(AudioManager.instance.GetRandomClip(AudioManager.instance.swordMissList));
    }





}

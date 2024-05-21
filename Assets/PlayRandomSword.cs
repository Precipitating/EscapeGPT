using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSword : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] swordMissSounds;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    public void Sword()
    {
        audioSource.PlayOneShot(swordMissSounds[UnityEngine.Random.Range(0, swordMissSounds.Length)]);
    }





}

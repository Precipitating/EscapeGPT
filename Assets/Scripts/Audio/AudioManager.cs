using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography.X509Certificates;


// Should only handle 2D sounds. 3D will be handled in their components.
public class AudioManager : MonoBehaviour
{
    // singleton
    public static AudioManager instance;

    public Sound[] globalSoundList, fleshCutList, swordMissList;
    public AudioSource sfxSource,musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Ambience");
    }

    public void PlayGlobalSFX(string name)
    {
        Sound sound = Array.Find(globalSoundList, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound not found!");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(globalSoundList, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound not found!");
        }
        else
        {
            sfxSource.clip = sound.clip;
            sfxSource.Play();
        }
    }

    public AudioClip GetGlobalSFX(string name)
    {
        return Array.Find(globalSoundList, x => x.name == name).clip;
    }

    public void PlayRandom2D(Sound[] list)
    {
        Sound sound = Array.Find(list, x => x.name == name);

        sfxSource.PlayOneShot(list[UnityEngine.Random.Range(0, list.Length)].clip);
    }








}

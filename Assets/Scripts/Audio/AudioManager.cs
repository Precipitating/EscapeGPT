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

    public Sound[] globalSoundList, fleshCutList, swordMissList, hurtList, footstepList, swordClashList;
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
        sfxSource.PlayOneShot(GetClip(name,globalSoundList));

    }

    public void PlayMusic(string name)
    {
        musicSource.clip = GetClip(name,globalSoundList);
        musicSource.Play();

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

    public AudioClip GetClip(string name, Sound[] list)
    {
        AudioClip result = null;
        Sound sound = Array.Find(list, x => x.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound not found!");
            
        }
        else
        {
            result = sound.clip;
        }


        return result;
    }

    public AudioClip GetGlobalClip(string name)
    {
        AudioClip result = null;
        Sound sound = Array.Find(globalSoundList, x => x.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound not found!");

        }
        else
        {
            result = sound.clip;
        }


        return result;
    }

    public AudioClip GetRandomClip(Sound[] list)
    {
        AudioClip result = null;

        result = list[UnityEngine.Random.Range(0, list.Length)].clip;

        return result;
    }










}

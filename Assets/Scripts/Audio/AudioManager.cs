using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// Should only handle 2D sounds. 3D will be handled in their components.
public class AudioManager : MonoBehaviour
{
    // singleton
    public static AudioManager instance;

    public Sound[] globalSoundList;
    public Sound[] swordSwingList;
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







}

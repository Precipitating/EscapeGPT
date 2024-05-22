using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootstep : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void Footstep(AnimationEvent val)
    {
        if (val.animatorClipInfo.weight > 0.5f)
        {
            audioSource.PlayOneShot(AudioManager.instance.GetRandomClip(AudioManager.instance.footstepList));
        }

    }
}

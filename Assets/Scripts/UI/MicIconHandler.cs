using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class MicIconHandler : MonoBehaviour
{
    [SerializeField] GameObject micIcon;
    bool isEnabled = false;
    private void OnEnable()
    {
        VoskSpeechToText.onToggleMic += EnableIcon;
    }

    private void OnDisable()
    {
        VoskSpeechToText.onToggleMic -= EnableIcon;
    }


    void EnableIcon()
    {
        isEnabled = !isEnabled;
        micIcon.SetActive(isEnabled);
    }
}

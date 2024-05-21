using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Voxell.Speech.TTS;
using UnityEngine.InputSystem;

public class ToggleMic : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private VoiceProcessor voiceProcessor;
    [SerializeField] private VoskSpeechToText vosk;
    [SerializeField] private VoiceDetected voiceDetected;
    bool isUsable = true;


    private void OnEnable()
    {
        TextToSpeech.onGuardFinishedSpeaking += ToggleUsable;
        voiceDetected.onGuardHearVoid += ToggleUsable;
    }

    private void OnDisable()
    {
        TextToSpeech.onGuardFinishedSpeaking -= ToggleUsable;
        voiceDetected.onGuardHearVoid -= ToggleUsable;
    }

    void Update()
    {
        // handle mic toggling
        if (Keyboard.current[Key.V].wasPressedThisFrame && !voiceProcessor.IsRecording)
        {
            if (isUsable)
            {      
                vosk.ToggleRecording();

            }

        }
    }

    void ToggleUsable()
    {
        isUsable = !isUsable;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voxell.Speech.TTS;

public class PlayTTS : MonoBehaviour
{
    [SerializeField] private TextToSpeech coreTTS;
    private void OnEnable()
    {
        // grab the guard's response
        VoiceDetected.onGuardHear += PlayGPTResult;
    }

    private void OnDisable()
    {
        VoiceDetected.onGuardHear += PlayGPTResult;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            coreTTS.Speak("Shut up prisoner or else I will kill you!");

        }
    }
    private void PlayGPTResult(string response)
    {
        coreTTS.Speak("Be quiet you gimp");
    }

}
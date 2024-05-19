using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Voxell.Speech.TTS;

public class PlayTTS : MonoBehaviour
{
    [SerializeField] private TextToSpeech coreTTS;
    private void OnEnable()
    {
        // grab the guard's response
        ChatGPTReceiver.onChatGPTResult += PlayGPTResult;
    }

    private void OnDisable()
    {
        ChatGPTReceiver.onChatGPTResult += PlayGPTResult;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!coreTTS.IsPlaying())
            {
                PlayDirectly("Be quiet you filthy disgusting prisoner");
            }
            

        }
    }
    private void PlayGPTResult(string response)
    {

        Debug.Log(response);

        if (response[0] == '1')
        {
            coreTTS.Speak(response.Substring(1));
        }
        else
        {
            coreTTS.Speak(response);
        }


    }


    public void PlayDirectly(string voiceline)
    {
        if (!coreTTS.IsPlaying())
        {
            coreTTS.Speak(voiceline);
        }
        

    }


}
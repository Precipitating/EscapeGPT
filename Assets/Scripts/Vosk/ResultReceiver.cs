using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultReceiver : MonoBehaviour
{
    [SerializeField] VoskSpeechToText VoskSpeechToText;
    public static event Action<string> onGuardHear;

    public RecognizedPhrase[] Phrases;

    private void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
    }


    // get the string result of what GPT replied with
    private void OnTranscriptionResult(string obj)
    {
        Debug.Log(JSONConverter(obj));
        onGuardHear?.Invoke(JSONConverter(obj));
        VoskSpeechToText.ToggleRecording();
    }


    private string JSONConverter(string json)
    {
        string result = "";
        string alternativesKey = "alternatives";

        JSONObject resultJson = JSONNode.Parse(json).AsObject;

        if (resultJson.HasKey(alternativesKey))
        {
            var alternatives = resultJson[alternativesKey].AsArray;
            Phrases = new RecognizedPhrase[alternatives.Count];

            for (int i = 0; i < Phrases.Length; i++)
            {
                Phrases[i] = new RecognizedPhrase(alternatives[i].AsObject);
            }

            result = Phrases[0].Text;


        }


        return result;
    }
}

public class RecognizedPhrase
{
    public const string TextKey = "text";

    public string Text = "";

    public RecognizedPhrase()
    {
    }

    public RecognizedPhrase(JSONObject json)
    {
        if (json.HasKey(TextKey))
        {
            //Vosk adds an extra space at the start of the string.
            Text = json[TextKey].Value.Trim();
        }
    }
}

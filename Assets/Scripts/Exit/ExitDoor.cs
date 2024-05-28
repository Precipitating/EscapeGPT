using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour, InteractableInterface
{
    public static event Action OnEscape;
    [SerializeField] private Guard guardScript;
    [SerializeField] private PlayTTS tts;
    [SerializeField] private string failedEscapeLine = "trying to escape while im on your tail, think again";
    


    public void Interact()
    {
        // to escape, either kill or lock the guard in.
        if (guardScript.GetState() == Guard.State.DEAD || guardScript.IsUnreachable())
        {
            OnEscape?.Invoke();
            AudioManager.instance.PlayGlobalSFX("ExitSuccess");
            AudioManager.instance.PlayMusic("Ambience");
        }
        else
        {
            AudioManager.instance.PlayGlobalSFX("ExitFail");
            tts.PlayDirectly(failedEscapeLine);
        }
            
    }
}

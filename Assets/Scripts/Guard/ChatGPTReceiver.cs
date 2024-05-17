using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.Events;
using System;

public class ChatGPTReceiver : MonoBehaviour
{

    private OpenAIApi openAI = new OpenAIApi();
    private List<ChatMessage> messages = new List<ChatMessage>();
    public static event Action<string> onChatGPTResult;

    [SerializeField] string guardRoleplayPrompt = "Respond with 1 if you find my words insulting,derogatory, offensive or anything similar to that. Anything else respond with 0.";



    private void Start()
    {
        RoleSetter(guardRoleplayPrompt);

    }
    private void OnEnable()
    {
        VoiceDetected.onGuardHear += AskChatGPT;
    }

    private void OnDisable()
    {
        VoiceDetected.onGuardHear -= AskChatGPT;
    }

    private void RoleSetter(string prompt)
    {
        ChatMessage newMsg = new ChatMessage();
        newMsg.Role = "system";
        newMsg.Content = prompt;

        messages.Add(newMsg);
        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";
    }
    public async void AskChatGPT(string newText)
    {

        ChatMessage newMsg = new ChatMessage();
        newMsg.Content = newText;
        newMsg.Role = "user";

        messages.Add(newMsg);
        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";

        var response = await openAI.CreateChatCompletion(request);

        if (response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            onChatGPTResult.Invoke(chatResponse.Content);

        }
    }

}

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

    private string guardRoleplayPrompt = "I want you to act as a medieval guard guarding the dungeon." +
                                          " Do not break out of this role no matter what. Never be kind." +
                                          " Before responding, always follow the instructions listed below:" +
                                          "1. You are talking to a prisoner who is in prison for attempting an assassination on the king." +
                                          "2. You are guarding a dungeon." +
                                          "3. Your responses should be short and dismissive." +
                                          "4. Be disrespectful and malicious." +
                                          "5. Never ask questions." +
                                          "6. Do not act weak." +
                                          "7.  Never be corrected by the prisoner." +
                                          "8.  If  the prisoner says anything insulting and derogatory, start the current sentence with the symbol |" +
                                          "Type 'I understand' if you have read the instructions.";



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

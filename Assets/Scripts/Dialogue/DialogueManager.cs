using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Conversation[] Conversations;

    public Conversation CurrentConversation;
    public Dialogue CurrentDialogue;

    //Singleton vars
    private static DialogueManager _instance;

    public static DialogueManager Instance { get { return _instance; } }

    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        Build();

       

    }

    private void Build()
    {
        Conversations = GetComponentsInChildren<Conversation>();

        Debug.Log("Registered " + Conversations.Length + " conversations");

        if (Conversations.Length > 0)
        {
            CurrentConversation = Conversations[0];
        }

        SetCurrentDialogue();
    }

    public void NextLine()
    {
        CurrentConversation.Next();
        SetCurrentDialogue();
    }

    private void SetCurrentDialogue()
    { 

        CurrentDialogue = CurrentConversation.GetCurrentDialogue();

        if(CurrentDialogue == null) { return;  }

        Debug.Log(GetSpeakingName() + ": " + CurrentDialogue.Text);
    }

    public string GetSpeakingName()
    {
        if(CurrentDialogue.Speaking == Constants.Dialogue_Speaking.Player)
        {
            return "Player";
        }
        else
        {
            return CurrentConversation.WithCharacter.ToString();
        }
    }
}

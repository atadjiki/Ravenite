using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    private Conversation[] Conversations;
    private int Index;

    public Conversation CurrentConversation;
    public Dialogue CurrentDialogue;
    public Choice CurrentChoice;

    public Constants.Conversation_Mode Mode;

    //Singleton vars
    private static ConversationManager _instance;

    public static ConversationManager Instance { get { return _instance; } }

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
        Index = 0;

        Debug.Log("Registered " + Conversations.Length + " conversations");

        if (Conversations.Length > 0)
        {
            CurrentConversation = Conversations[Index];
        }

        CurrentChoice = CurrentConversation.GetCurrentChoice();
        CurrentDialogue = CurrentConversation.GetCurrentDialogue();

        if (Mode == Constants.Conversation_Mode.Dialogue)
        {
            SetCurrentDialogue();
        }
        else if(Mode == Constants.Conversation_Mode.Choice)
        {
            SetCurrentChoices();
        }
        
    }

    public void NextLine()
    {
        if (CurrentConversation.IsDialogueAvailable())
        {
            if (ConversationManager.Instance.Mode == Constants.Conversation_Mode.Dialogue)
            {
                CurrentConversation.NextLine();
                SetCurrentDialogue();
            }
            else
            {
                Debug.Log("Not in dialogue mode!");
            }
        }
        else
        {
            Debug.Log("No dialogue available!");
            if(Mode == Constants.Conversation_Mode.Dialogue)
            {
                Mode = Constants.Conversation_Mode.Choice;
                SetCurrentChoices();
            }
        }
    }

    public void NextNode(Constants.Choice choice)
    {

        if (CurrentConversation.AreChoicesAvailable())
        {
            if (ConversationManager.Instance.Mode == Constants.Conversation_Mode.Choice)
            {
                CurrentConversation.NextNode(choice);
                SetCurrentChoices();
                StateManager.Instance.AddChoiceFlag(CurrentChoice.Flag);
            }
            else
            {
                Debug.Log("Not in choice mode!");
            }
        }
        else
        {
            Debug.Log("No choices available!");
            NextConversation();

        }
    }

    public void NextConversation()
    {
        if (Index + 1 < Conversations.Length-1)
        {
            Index++;
            CurrentConversation = Conversations[Index];
        }
        else
        {
            Debug.Log("No more conversations!");
        }
    }

    private void SetCurrentDialogue()
    {
        SubtitleManager.Instance.SubtitleMode();

        CurrentDialogue = CurrentConversation.GetCurrentDialogue();

        if(CurrentDialogue == null) { return;  }

        Debug.Log(GetSpeakingName() + ": " + CurrentDialogue.Text);
        SubtitleManager.Instance.SetSegmentedText(Constants.Text_Type.Dialogue, GetSpeakingName() + ": ", CurrentDialogue.Text);
    }

    private void SetCurrentChoices()
    {
        SubtitleManager.Instance.ChoiceMode();

        CurrentChoice = CurrentConversation.GetCurrentChoice();

        if(CurrentChoice == null) { return;  }

        if(CurrentChoice.A != null)
        {
           Debug.Log("New choice: " + CurrentChoice.A.Flag.ToString());
           SubtitleManager.Instance.SetText(Constants.Text_Type.ChoiceA, CurrentChoice.A.Text);
        }
        if (CurrentChoice.B != null)
        {
            Debug.Log("New choice: " + CurrentChoice.B.Flag.ToString());
            SubtitleManager.Instance.SetText(Constants.Text_Type.ChoiceB, CurrentChoice.B.Text);
        }
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

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
        Debug.Log(this.gameObject.name + " Initialized");
    }

    private void Build()
    {

        Conversations = GetComponentsInChildren<Conversation>();
        Index = -1;

        Debug.Log("Registered " + Conversations.Length + " conversations");
    }

    public void SetDefaultDialogue()
    {
        if (Mode == Constants.Conversation_Mode.Dialogue)
        {
            SetCurrentDialogue();
        }
        else if (Mode == Constants.Conversation_Mode.Choice)
        {
            SetCurrentChoices();
        }
    }

    public bool NextLine()
    {
        if (CurrentConversation.IsDialogueAvailable())
        {
            if (ConversationManager.Instance.Mode == Constants.Conversation_Mode.Dialogue)
            {
                Debug.Log("Next Line!");
                CurrentConversation.NextLine();
                SetCurrentDialogue();
                return true;
            }
            else
            {
                Debug.Log("Not in dialogue mode!");
                return false;
            }
        }
        else
        {
            
            if(Mode == Constants.Conversation_Mode.Dialogue && CurrentConversation.AreChoicesAvailable())
            {
                Debug.Log("No dialogue available! Switching to choice mode");
                Mode = Constants.Conversation_Mode.Choice;
                SetCurrentChoices();
            }
            else if(Mode == Constants.Conversation_Mode.Dialogue && CurrentConversation.AreChoiceTreesAvailable())
            {
                Debug.Log("No dialogue available! Switching to the next choice tree");
                Mode = Constants.Conversation_Mode.Choice;
                CurrentConversation.NextChoiceTree();
                SetCurrentChoices();
                GameState.Instance.StartNextChoiceTree();
            }
            else if(Mode == Constants.Conversation_Mode.Dialogue && CurrentConversation.AreChoicesAvailable() == false)
            {
                GameState.Instance.ConversationFinished();
                Debug.Log("No dialogue available! Ending conversation");
            }

            return false;
        }
    }

    public bool NextNode(Constants.Choice choice)
    {
        bool flag = false;

        if (CurrentConversation.AreChoicesAvailable())
        {
            if (ConversationManager.Instance.Mode == Constants.Conversation_Mode.Choice)
            {
                CurrentConversation.NextNode(choice);
                SetCurrentChoices();
                flag = true;
            }
            else
            {
                Debug.Log("Not in choice mode!");
                flag = false;
            }
        }

        //do another check to validate the choice tree
        if(CurrentConversation.AreChoicesAvailable() == false && CurrentConversation.AreDialogueSetsAvailable() == false)
        {
            GameState.Instance.ConversationFinished();
            NextConversation();
            flag = false;
        }
        else if (CurrentConversation.AreChoicesAvailable() == false && CurrentConversation.AreDialogueSetsAvailable() == true)
        {
            //move to next dialogue set
            CurrentConversation.NextDialogueSet();
            Mode = Constants.Conversation_Mode.Dialogue;
            flag = false;
            GameState.Instance.StartNextDialogueSet();
            SetCurrentDialogue();
            
        }

        return flag;

    }

    public void ChoiceAPressed()
    {
        Debug.Log("Choice A Pressed");
        NextNode(Constants.Choice.A);
    }

    public void ChoiceBPressed()
    {
        Debug.Log("Choice B Pressed");
        NextNode(Constants.Choice.B);
    }

    public bool AreConversationsAvailable()
    {
        return Index == -1 || Index + 1 < Conversations.Length - 1;
    }

    public void NextConversation()
    {
        if (AreConversationsAvailable())
        {
            Index++;
            CurrentConversation = Conversations[Index];
            
            SetCurrentChoices();
            SetCurrentDialogue();
        }
        else
        {
            Debug.Log("No more conversations!");
            UIManager.Instance.AllOff();
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

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
                CurrentConversation.NextLine();
                SetCurrentDialogue();
                return true;
            }
            else
            {
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
        //Debug.Log("Choice A Pressed");
        NextNode(Constants.Choice.A);
    }

    public void ChoiceBPressed()
    {
        //Debug.Log("Choice B Pressed");
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

        CurrentDialogue = CurrentConversation.GetCurrentDialogue();

        if(CurrentDialogue == null) { return;  }

        SubtitleManager.Instance.SetText(GetSpeakingName() + ": " + CurrentDialogue.Text);
    }

    private void SetCurrentChoices()
    {

        CurrentChoice = CurrentConversation.GetCurrentChoice();

        string result = "";
        string ChoiceAControl;
        string ChoiceBControl;

        if (CurrentChoice == null) { return;  }

        if(CurrentChoice.A != null && CurrentChoice.B == null)
        {
           ChoiceAControl = InputManager.ChoiceA.ToString();
           result += ChoiceAControl + ": " + CurrentChoice.A.Text;
            
        }
        else if (CurrentChoice.A == null && CurrentChoice.B != null)
        {
            ChoiceBControl = InputManager.ChoiceB.ToString();
            result += ChoiceBControl + ": " + CurrentChoice.B.Text;
        }
        else if(CurrentChoice.A != null && CurrentChoice.B != null)
        {
            ChoiceAControl = InputManager.ChoiceA.ToString();
            ChoiceBControl = InputManager.ChoiceB.ToString();

            result += ChoiceAControl + ": " + CurrentChoice.A.Text;
            result += "\n\n";
            result += ChoiceBControl + ": " + CurrentChoice.B.Text;
        }

        SubtitleManager.Instance.SetText(result);


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

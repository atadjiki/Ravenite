using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    private Conversation[] Conversations;
    private int Index;

    public Conversation CurrentConversation;

    public List<Conversation> PreviousConversations;

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

        PreviousConversations = new List<Conversation>();
    }

    public void Next(Constants.Choice Choice)
    {
        if(CurrentConversation.CurrentNode.Next == null)
        {
            GameState.Instance.ConversationFinished();
        }
        else
        {
            if (Choice == Constants.Choice.None)
            {
                NextLine();
            }
            else
            {
                NextChoice(Choice);
            }
        }

        
    }

    private void NextLine()
    {
        if (IsDialogue())
        {
            CurrentConversation.CurrentNode = CurrentConversation.CurrentNode.Next;
            ResolveUI();
        }
    }

    private void NextChoice(Constants.Choice Choice)
    {
        if (IsChoice())
        {
            if (Choice == Constants.Choice.B)
            {
                CurrentConversation.CurrentNode = CurrentConversation.CurrentNode.gameObject.GetComponent<Choice>().Alternate;
            }
            else if(Choice == Constants.Choice.A)
            {
                CurrentConversation.CurrentNode = CurrentConversation.CurrentNode.Next;
            }

            ResolveUI();
        }
    }

    public bool AreConversationsAvailable()
    {
        if (Index < Conversations.Length - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void NextConversation()
    {
        if (AreConversationsAvailable())
        {
            Index++;
            CurrentConversation = Conversations[Index];
            ResolveUI();
        }
        else
        {
            Debug.Log("No more conversations!");
            UIManager.Instance.AllOff();
        }
    }

    public void ResolveUI()
    {
        if (IsDialogue())
        {
            SetCurrentDialogue();
        }
        else if (IsChoice())
        {
            SetCurrentChoices();
        }
    }

    private void SetCurrentDialogue()
    {
        SubtitleManager.Instance.SetText(GetSpeakingName() + ": " + CurrentConversation.CurrentNode.Text);
    }

    private void SetCurrentChoices()
    {
        Choice CurrentChoice = CurrentConversation.CurrentNode.gameObject.GetComponent<Choice>();

        string result = "";
        string ChoiceAControl;
        string ChoiceBControl;

        if (CurrentChoice == null) { return; }

        if (CurrentChoice.A != null && CurrentChoice.B == null)
        {
            ChoiceAControl = InputManager.ChoiceA.ToString();
            result += ChoiceAControl + ": " + CurrentChoice.A;

        }
        else if (CurrentChoice.A == null && CurrentChoice.B != null)
        {
            ChoiceBControl = InputManager.ChoiceB.ToString();
            result += ChoiceBControl + ": " + CurrentChoice.B;
        }
        else if (CurrentChoice.A != null && CurrentChoice.B != null)
        {
            ChoiceAControl = InputManager.ChoiceA.ToString();
            ChoiceBControl = InputManager.ChoiceB.ToString();

            result += ChoiceAControl + ": " + CurrentChoice.A;
            result += "\n\n";
            result += ChoiceBControl + ": " + CurrentChoice.B;
        }

        SubtitleManager.Instance.SetText(result);


    }

    public string GetSpeakingName()
    {
        if (CurrentConversation.CurrentNode.gameObject.GetComponent<Dialogue>().Speaking == Constants.Dialogue_Speaking.Player)
        {
            return CharacterManager.Instance.GetFirstCharacterName(Constants.Character_Names.Player);
        }
        else
        {
            return CharacterManager.Instance.GetFirstCharacterName(CurrentConversation.WithCharacter);
        }
    }

    public void StashLatestConversation()
    {
        if (PreviousConversations.Contains(CurrentConversation) == false)
        {
            PreviousConversations.Add(CurrentConversation);
        }
    }

    public bool IsChoice()
    {
        if (CurrentConversation.CurrentNode.gameObject.GetComponent<Choice>() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsDialogue()
    {
        if (CurrentConversation.CurrentNode.gameObject.GetComponent<Dialogue>() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Flags.Choices GetFlag()
    {
        if (IsChoice())
        {
            return CurrentConversation.CurrentNode.gameObject.GetComponent<Choice>().Flag;
        }
        else
        {
            return Flags.Choices.None;
        }
    }

}

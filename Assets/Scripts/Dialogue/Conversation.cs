using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains the character the player is speaking to, and a queue of all 
// dialogue and choices
public class Conversation : MonoBehaviour
{
    public Constants.Character_Names WithCharacter;

    //Dialogue variables
    private DialogueSet[] DialogueSets; //collection of arrays containing lines of dialogue
    public DialogueSet CurrentDialogueSet; //the current dialogue array
    public Dialogue CurrentDialogueLine; //the current index within the current dialogue array

    public int SetIndex = 0; //index of current set
    public int LineIndex = 0; //index of current line in current set

    //Choice making variables
    private Choice CurrentRoot;
    private Choice CurrentChoice;



    private void Awake()
    {

        DialogueSets = GetComponentsInChildren<DialogueSet>();

        Debug.Log("Registered " + DialogueSets.Length + " sets of dialogue");

        //setup dialogue stuff
        if(DialogueSets.Length > 0)
        {
            CurrentDialogueSet = DialogueSets[SetIndex];
        }

        InitializeDialogueLine();

        //choice tree
        CurrentRoot = GetComponentInChildren<Choice>();
        CurrentChoice = CurrentRoot;
    }

    public void InitializeDialogueLine()
    {

        LineIndex = 0;

        if (CurrentDialogueSet.Dialogue.Length > 0)
        {
            CurrentDialogueLine = CurrentDialogueSet.Dialogue[LineIndex];
        }

        Debug.Log("Registered " + CurrentDialogueSet.Dialogue.Length + " lines of dialogue in current conversation");
    }

    public void NextDialogueSet()
    {
        if(AreDialogueSetsAvailable())
        {
            SetIndex++;
            CurrentDialogueSet = DialogueSets[SetIndex];
        }

       

        InitializeDialogueLine();

        Debug.Log("New Dialogue Set: " + CurrentDialogueSet.name);
    }


    public void NextLine()
    {

        if (IsDialogueAvailable())
        {
            LineIndex++;
            CurrentDialogueLine = CurrentDialogueSet.Dialogue[LineIndex];
        }

        return;

    }

    public void NextNode(Constants.Choice Choice)
    {

        if (Choice == Constants.Choice.A)
        {
            CurrentChoice = CurrentChoice.A;
        }
        else if (Choice == Constants.Choice.B)
        {
            CurrentChoice = CurrentChoice.B;
        }

        return;

    }

    public bool IsDialogueAvailable()
    {
        if(LineIndex < CurrentDialogueSet.Dialogue.Length-1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AreDialogueSetsAvailable()
    {
        if(SetIndex < DialogueSets.Length - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AreChoicesAvailable()
    {
        if(CurrentChoice.A != null || CurrentChoice.B != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public DialogueSet GetDialogueSet()
    {
        return CurrentDialogueSet;
    }

    public Dialogue GetCurrentDialogue()
    {
        return CurrentDialogueLine;
    }

    public Choice GetCurrentChoice()
    {
        return CurrentChoice;
    }
}

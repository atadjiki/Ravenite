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
    private Dialogue CurrentDialogueLine; //the current index within the current dialogue array

    public int SetIndex = 0; //index of current set
    public int LineIndex = 0; //index of current line in current set

    //Choice making variables
    private Choice CurrentRoot;
    private Choice CurrentChoice;



    private void Awake()
    {

        DialogueSets = GetComponentsInChildren<DialogueSet>();

        //setup dialogue stuff
        if(DialogueSets.Length > 0)
        {
            CurrentDialogueSet = DialogueSets[SetIndex];
        }
        
        Debug.Log("Registered " + CurrentDialogueSet.Dialogue.Length + " lines of dialogue in conversation");

        if (CurrentDialogueSet.Dialogue.Length > 0)
        {
            CurrentDialogueLine = CurrentDialogueSet.Dialogue[LineIndex];
        }

        //choice tree
        CurrentRoot = GetComponentInChildren<Choice>();
        CurrentChoice = CurrentRoot;
    }

    public void NextLine()
    {

        if (LineIndex <CurrentDialogueSet.Dialogue.Length - 1)
        {
            LineIndex++;
            CurrentDialogueLine = CurrentDialogueSet.Dialogue[LineIndex];
        }

        return;

    }

    public Dialogue GetCurrentDialogue()
    {
        return CurrentDialogueLine;
    }

    public Choice GetCurrentChoice()
    {
        return CurrentChoice;
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

        Debug.Log("Moved to new choice node: " + CurrentChoice.Flag.ToString());
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
}

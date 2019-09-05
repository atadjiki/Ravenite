using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains the character the player is speaking to, and a queue of all 
// dialogue and choices
public class Conversation : MonoBehaviour
{
    public Constants.Character_Names WithCharacter;

    //Dialogue variables
    private Dialogue[] Dialogue;
    private Dialogue CurrentDialogue;
    public int Index = 0;
    private int Length;

    //Choice making variables
    private Choice Root;
    private Choice CurrentChoice;



    private void Awake()
    {
        //setup dialogue stuff
        Dialogue = GetComponentsInChildren<Dialogue>();
        Length = Dialogue.Length;

        Debug.Log("Registered " + Dialogue.Length + " lines of dialogue in conversation");

        if (Length > 0)
        {
            CurrentDialogue = Dialogue[Index];
        }

        //choice tree
        Root = GetComponentInChildren<Choice>();
        CurrentChoice = Root;
    }

    public void NextLine()
    {

        if (Index < Length - 1)
        {
            Index++;
            CurrentDialogue = Dialogue[Index];
        }

        return;

    }

    public Dialogue GetCurrentDialogue()
    {
        return CurrentDialogue;
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
        if(Index < Length-1)
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

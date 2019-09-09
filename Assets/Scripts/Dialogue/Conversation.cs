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
    public ChoiceTree CurrentChoiceTree;

    public int SetIndex = 0; //index of current set
    public int LineIndex = 0; //index of current line in current set
    public int TreeIndex = 0;

    private ChoiceTree[] ChoiceTrees;
    

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

        ChoiceTrees = GetComponentsInChildren<ChoiceTree>();

        Debug.Log("Registered " + ChoiceTrees.Length + " choice trees");

        if(ChoiceTrees.Length > 0)
        {
            CurrentChoiceTree = ChoiceTrees[TreeIndex];
        }
    }

    public void InitializeDialogueLine()
    {

        LineIndex = 0;

        Debug.Log("Registered " + CurrentDialogueSet.Dialogue.Length + " lines of dialogue in current conversation");
    }

    public void NextChoiceTree()
    {
        if (AreChoiceTreesAvailable())
        {
            TreeIndex++;
            CurrentChoiceTree = ChoiceTrees[TreeIndex];
        }

        Debug.Log("New Choice Tree: " + CurrentChoiceTree.name);
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
        }

        return;

    }

    public void NextNode(Constants.Choice Choice)
    {

        if (Choice == Constants.Choice.A)
        {
            CurrentChoiceTree.CurrentChoice = CurrentChoiceTree.CurrentChoice.A;
        }
        else if (Choice == Constants.Choice.B)
        {
            CurrentChoiceTree.CurrentChoice = CurrentChoiceTree.CurrentChoice.B;
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

    public bool AreChoiceTreesAvailable()
    {
        if(TreeIndex < ChoiceTrees.Length - 1)
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
        if(CurrentChoiceTree.CurrentChoice.A != null || CurrentChoiceTree.CurrentChoice.B != null)
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
        return CurrentDialogueSet.Dialogue[LineIndex];
    }

    public Choice GetCurrentChoice()
    {
        return CurrentChoiceTree.CurrentChoice;
    }
}

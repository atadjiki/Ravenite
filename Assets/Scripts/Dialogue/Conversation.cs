using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains the character the player is speaking to, and a queue of all 
// dialogue and choices
public class Conversation : MonoBehaviour
{
    public Constants.Character_Names WithCharacter;
    private Dialogue[] Dialogue;
    private Dialogue CurrentDialogue;
    public int Index = 0;
    private int Length;


    private void Awake()
    {
        Dialogue = GetComponents<Dialogue>();
        Length = Dialogue.Length;

        Debug.Log("Registered " + Dialogue.Length + " lines of dialogue in conversation");

        if(Length > 0)
        {
            CurrentDialogue = Dialogue[Index];
        }
    }

    public Dialogue Next()
    {
        if(Index < Length-1)
        {
            Index++;
            CurrentDialogue = Dialogue[Index];
            return CurrentDialogue;
        }
        else
        {
            return null;
        }
    }

    public Dialogue GetCurrentDialogue()
    {
        return CurrentDialogue;
    }
}

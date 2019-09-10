using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains the character the player is speaking to, and a queue of all 
// dialogue and choices
public class Conversation : MonoBehaviour
{
    public Constants.Character_Names WithCharacter;

    public Flags.Choices FinalFlag = Flags.Choices.None;
    public Constants.Faction FinalFaction = Constants.Faction.Player;
    public Constants.Modifier FinalModifier = Constants.Modifier.None;

    public string Summary;
    public string Goal;

    public Node CurrentNode;
}

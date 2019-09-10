using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //Singleton vars
    private static CharacterManager _instance;

    public static CharacterManager Instance { get { return _instance; } }

    private Character[] Characters;

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
        //Debug.Log(this.gameObject.name + " Initialized");
    }

    void Build()
    {
        Characters = GetComponentsInChildren<Character>();
        //Debug.Log("Registered " + Characters.Length + " characters");
    }

    public Character GetCharacter(Constants.Character_Names Name)
    {
        foreach(Character c in Characters)
        {
            if(Name == c.Name)
            {
                return c;
            }
        }

        return null;
    }

    public string GetFullCharacterName(Constants.Character_Names Name)
    {
        if (Name == Constants.Character_Names.Eliza_Kirkwood)
        {
            return "Eliza Kirkwood";
        }
        else if (Name == Constants.Character_Names.Isaac_Cantwell)
        {
            return "Isaac Cantwell";
        }
        else if (Name == Constants.Character_Names.Stacy_Thatcher)
        {
            return "Stacy Thatcher";
        }
        else if (Name == Constants.Character_Names.Player)
        {
            return "Me";
        }
        else
        {
            return "";
        }
    }

    public string GetFirstCharacterName(Constants.Character_Names Name)
    {
        if (Name == Constants.Character_Names.Eliza_Kirkwood)
        {
            return "Eliza";
        }
        else if (Name == Constants.Character_Names.Isaac_Cantwell)
        {
            return "Isaac";
        }
        else if (Name == Constants.Character_Names.Stacy_Thatcher)
        {
            return "Stacy";
        }
        else if (Name == Constants.Character_Names.Player)
        {
            return "Me";
        }
        else
        {
            return "";
        }
    }
}

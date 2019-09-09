using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceTree : MonoBehaviour
{
    //Choice making variables
    public Choice Root;
    public Choice CurrentChoice;

    private void Awake()
    {
        Root = GetComponentInChildren<Choice>();
        CurrentChoice = Root;
    }

}

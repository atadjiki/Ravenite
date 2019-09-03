using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Keeps track of the player's state in terms of their relationships
 */

public class StateManager : MonoBehaviour
{
    private const int value_range = 3;

    [SerializeField]
    [Range(-1*value_range,value_range)]
    private int Cop_State = 0;
    [SerializeField]
    [Range(-1 * value_range, value_range)]
    private int Family_State = 0;
    [SerializeField]
    [Range(-1 * value_range, value_range)]
    private int Rivals_State = 0;

    private int Previous_Cops = 0;
    private int Previous_Rivals = 0;
    private int Previous_Family = 0;

    [Header("Debug")]
    public Constants.Faction Debug_State;
    public Constants.Modifier Debug_Action;

    //Singleton vars
    private static StateManager _instance;

    public static StateManager Instance { get { return _instance; } }


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
    }

    private void Update()
    {
        if(Previous_Cops != Cop_State)
        {
            PrintStates();
            Previous_Cops = Cop_State;
        }
        else if(Previous_Family != Family_State)
        {
            PrintStates();
            Previous_Family = Family_State;
        }
        else if (Previous_Rivals != Rivals_State)
        {
            PrintStates();
            Previous_Rivals = Rivals_State;
        }
    }

    public int GetState(Constants.Faction Type)
    {
        if(Type == Constants.Faction.Cops)
        {
            return Cop_State;
        }
        else if(Type == Constants.Faction.Family)
        {
            return Family_State;
        }
        else if(Type == Constants.Faction.Rival)
        {
            return Rivals_State;
        }
        else
        {
            return 0;
        }
    }

    public void SetState(Constants.Faction type, int value)
    {
        if(value > value_range || value < (-1 * value_range))
        {
            Debug.Log("Value not in range!");
            return;
        }

        if(type == Constants.Faction.Cops)
        {
            Cop_State = value;
            Previous_Cops = value;
        }
        else if(type == Constants.Faction.Family)
        {
            Family_State = value;
            Previous_Family = value;
        }
        else if(type == Constants.Faction.Rival)
        {
            Rivals_State = value;
            Previous_Rivals = value;
        }
    }

    public void SetState(Constants.Faction type, Constants.Modifier action)
    {
        if(type == Constants.Faction.Cops)
        {
            if(action == Constants.Modifier.Decrement)
            {
                Cop_State--;
                Previous_Cops = Cop_State;
            }
            else if(action == Constants.Modifier.Increment)
            {
                Cop_State++;
                Previous_Cops = Cop_State;
            }
        }
        else if (type == Constants.Faction.Family)
        {
            if (action == Constants.Modifier.Decrement)
            {
                Family_State--;
                Previous_Family = Family_State;
            }
            else if (action == Constants.Modifier.Increment)
            {
                Family_State++;
                Previous_Family = Family_State;

            }
        }
        else if (type == Constants.Faction.Rival)
        {
            if (action == Constants.Modifier.Decrement)
            {
                Rivals_State--;
                Previous_Rivals = Rivals_State;
            }
            else if (action == Constants.Modifier.Increment)
            {
                Rivals_State++;
                Previous_Rivals = Rivals_State;
            }
        }

        PrintStates();
    }

    public void PrintStates()
    {

        Debug.Log("States\n" +
                  "------\n" +
                  "Cops:    " + Cop_State + "\n" +
                  "Family:    " + Family_State + "\n" +
                  "Rivals:    " + Rivals_State + "\n");
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Keeps track of the player's state in terms of their relationships
 */

public class StateManager : MonoBehaviour
{

    public enum State_Type { Cops, Family, Rival };
    public enum Action_Type { Decrement, Increment };

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
    public State_Type Debug_State;
    public Action_Type Debug_Action;

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

    public int GetState(State_Type Type)
    {
        if(Type == State_Type.Cops)
        {
            return Cop_State;
        }
        else if(Type == State_Type.Family)
        {
            return Family_State;
        }
        else if(Type == State_Type.Rival)
        {
            return Rivals_State;
        }
        else
        {
            return 0;
        }
    }

    public void SetState(State_Type type, int value)
    {
        if(value > value_range || value < (-1 * value_range))
        {
            Debug.Log("Value not in range!");
            return;
        }

        if(type == State_Type.Cops)
        {
            Cop_State = value;
            Previous_Cops = value;
        }
        else if(type == State_Type.Family)
        {
            Family_State = value;
            Previous_Family = value;
        }
        else if(type == State_Type.Rival)
        {
            Rivals_State = value;
            Previous_Rivals = value;
        }
    }

    public void SetState(State_Type type, Action_Type action)
    {
        if(type == State_Type.Cops)
        {
            if(action == Action_Type.Decrement)
            {
                Cop_State--;
                Previous_Cops = Cop_State;
            }
            else if(action == Action_Type.Increment)
            {
                Cop_State++;
                Previous_Cops = Cop_State;
            }
        }
        else if (type == State_Type.Family)
        {
            if (action == Action_Type.Decrement)
            {
                Family_State--;
                Previous_Family = Family_State;
            }
            else if (action == Action_Type.Increment)
            {
                Family_State++;
                Previous_Family = Family_State;

            }
        }
        else if (type == State_Type.Rival)
        {
            if (action == Action_Type.Decrement)
            {
                Rivals_State--;
                Previous_Rivals = Rivals_State;
            }
            else if (action == Action_Type.Increment)
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

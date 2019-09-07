﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Keeps track of the player's state in terms of their relationships
 */

public class StateManager : MonoBehaviour
{
    private const float value_range = 3;

    [SerializeField]
    [Range(-1*value_range,value_range)]
    private float Cop_State = 0;
    [SerializeField]
    [Range(-1 * value_range, value_range)]
    private float Neighborhood_State = 0;
    [SerializeField]
    [Range(-1 * value_range, value_range)]
    private float Rivals_State = 0;

    private float Previous_Cops = 0;
    private float Previous_Rivals = 0;
    private float Previous_Neighborhood = 0;

    private HashSet<Flags.Choices> Choices;

    [Header("Debug")]
    public Constants.Faction Debug_State;
    public Constants.Modifier Debug_Action;

    public bool Started = false;

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

        Build();
    }

    private void Build()
    {
        Choices = new HashSet<Flags.Choices>();
        StartMenu();
    }

    private void Update()
    {
        if(Previous_Cops != Cop_State)
        {
            Previous_Cops = Cop_State;
            LedgerUpdater.Instance.SetFill(Constants.Faction.Cops);

        }
        else if(Previous_Neighborhood != Neighborhood_State)
        {
            Previous_Neighborhood = Neighborhood_State;
            LedgerUpdater.Instance.SetFill(Constants.Faction.Neighborhood);
        }
        else if (Previous_Rivals != Rivals_State)
        {
            Previous_Rivals = Rivals_State;
            LedgerUpdater.Instance.SetFill(Constants.Faction.Rivals);
        }
    }

    public float GetState(Constants.Faction Type)
    {
        if(Type == Constants.Faction.Cops)
        {
            return Cop_State;
        }
        else if(Type == Constants.Faction.Neighborhood)
        {
            return Neighborhood_State;
        }
        else if(Type == Constants.Faction.Rivals)
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
            LedgerUpdater.Instance.SetFill(Constants.Faction.Cops);

        }
        else if(type == Constants.Faction.Neighborhood)
        {
            Neighborhood_State = value;
            Previous_Neighborhood = value;
            LedgerUpdater.Instance.SetFill(Constants.Faction.Neighborhood);

        }
        else if(type == Constants.Faction.Rivals)
        {
            Rivals_State = value;
            Previous_Rivals = value;
            LedgerUpdater.Instance.SetFill(Constants.Faction.Rivals);
        }
    }

    public void SetState(Constants.Faction type, Constants.Modifier action)
    {
        if(type == Constants.Faction.Cops)
        {
            if(action == Constants.Modifier.Decrement)
            {
                Cop_State--;
            }
            else if(action == Constants.Modifier.Increment)
            {
                Cop_State++;
            }
            Cop_State = CapValue(Cop_State);
            Previous_Cops = Cop_State;
            LedgerUpdater.Instance.SetFill(Constants.Faction.Cops);
        }
        else if (type == Constants.Faction.Neighborhood)
        {
            if (action == Constants.Modifier.Decrement)
            {
                Neighborhood_State--; 
            }
            else if (action == Constants.Modifier.Increment)
            {
                Neighborhood_State++;
            }

            Neighborhood_State = CapValue(Neighborhood_State);
            Previous_Neighborhood = Neighborhood_State;
            LedgerUpdater.Instance.SetFill(Constants.Faction.Neighborhood);
        }
        else if (type == Constants.Faction.Rivals)
        {
            if (action == Constants.Modifier.Decrement)
            {
                Rivals_State--;
            }
            else if (action == Constants.Modifier.Increment)
            {
                Rivals_State++;
            }

            Rivals_State = CapValue(Rivals_State);
            Previous_Rivals = Rivals_State;
            LedgerUpdater.Instance.SetFill(Constants.Faction.Rivals);
        }
    }

    public void PrintStates()
    {

        Debug.Log("States\n" +
                  "------\n" +
                  "Cops:    " + Cop_State + "\n" +
                  "Family:    " + Neighborhood_State + "\n" +
                  "Rivals:    " + Rivals_State + "\n");
    }

    public void PrintFlags()
    {
        string flags = "";
        foreach(Flags.Choices Choice in Choices)
        {
            flags += Choice.ToString() + " \n";
        }
        Debug.Log("Flags\n" + flags);
    }

    public void AddChoiceFlag(Flags.Choices Flag)
    {
        Choices.Add(Flag);
        Debug.Log("Flag added: " + Flag.ToString());
    }

    public void StartGame()
    {
        Started = true;
        CameraRig.Instance.SwitchToMain();
        UIManager.Instance.SwitchToPromptPanel();

    }

    public void StartMenu()
    {
        Started = false;
        CameraRig.Instance.SwitchToStart();
        UIManager.Instance.SwitchToStartPanel();
    }

    public float GetNormalizedValue(Constants.Faction Faction)
    {
        if(Faction == Constants.Faction.Neighborhood)
        {
            return (Neighborhood_State+value_range) / (2*value_range);
        }
        else if(Faction == Constants.Faction.Cops)
        {
            return (Cop_State + value_range) / (2 * value_range);
        }
        else if(Faction == Constants.Faction.Rivals)
        {
            return (Rivals_State + value_range) / (2 * value_range);
        }
        else
        {
            return 0;
        }
    }

    private float CapValue(float value)
    {
        if(value > value_range)
        {
            return value_range;
        }
        else if(value < -1 * value_range)
        {
            return  -1 * value_range;
        }
        else
        {
            return value;
        }
    }
}

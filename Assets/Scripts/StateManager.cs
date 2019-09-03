using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Keeps track of the player's state in terms of their relationships
 */

public class StateManager : MonoBehaviour
{

    private const int value_range = 3;

    [Range(-1 * value_range, value_range)]
    public int Cop_State = 0;
    [Range(-1 * value_range, value_range)]
    public int Family_State = 0;
    [Range(-1 * value_range, value_range)]
    public int Rival_State = 0;


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
}

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(StateManager))]
public class StateManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Modify State"))
        {
            StateManager.Instance.SetState(StateManager.Instance.Debug_State, StateManager.Instance.Debug_Action);
        }

        if(GUILayout.Button("Print Flags"))
        {
            StateManager.Instance.PrintFlags();
        }

        if (GUILayout.Button("Print States"))
        {
            StateManager.Instance.PrintStates();
        }
    }
}

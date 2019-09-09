using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ReputationManager))]
public class StateManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Modify State"))
        {
            ReputationManager.Instance.SetState(ReputationManager.Instance.Debug_State, ReputationManager.Instance.Debug_Action);
        }

        if(GUILayout.Button("Print Flags"))
        {
            ReputationManager.Instance.PrintFlags();
        }

        if (GUILayout.Button("Print States"))
        {
            ReputationManager.Instance.PrintStates();
        }
    }
}

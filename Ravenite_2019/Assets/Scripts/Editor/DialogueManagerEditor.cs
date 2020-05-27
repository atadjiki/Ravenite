using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ConversationManager))]
public class DialogueManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Next Line"))
        {
            GameState.Instance.NextLine();
        }

        if (GUILayout.Button("Choice A"))
        {
            GameState.Instance.NextChoice(Constants.Choice.A);
        }

        if (GUILayout.Button("Choice B"))
        {
            GameState.Instance.NextChoice(Constants.Choice.B);
        }
    }
}

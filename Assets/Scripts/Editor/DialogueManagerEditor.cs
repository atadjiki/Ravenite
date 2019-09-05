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
            ConversationManager.Instance.NextLine();
        }

        if (GUILayout.Button("Choice A"))
        {
            ConversationManager.Instance.NextNode(Constants.Choice.A);
        }

        if (GUILayout.Button("Choice B"))
        {
            ConversationManager.Instance.NextNode(Constants.Choice.B);
        }
    }
}

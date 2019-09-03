using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DialogueManager))]
public class DialogueManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Next Line"))
        {
            DialogueManager.Instance.NextLine();
        }

        //if (GUILayout.Button("Positive Choice"))
        //{
        //}

        //if (GUILayout.Button("Negative Choice"))
        //{
        //}
    }
}

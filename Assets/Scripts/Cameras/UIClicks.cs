using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClicks : MonoBehaviour
{
    public enum UI_Input { CHOICE_A, CHOICE_B, DIALOGUE};

    public UI_Input InputType;

    void OnMouseDown()
    {
        if (InputType == UI_Input.CHOICE_A)
        {
            ConversationManager.Instance.NextNode(Constants.Choice.A);
        }
        else if (InputType == UI_Input.CHOICE_B)
        {
            ConversationManager.Instance.NextNode(Constants.Choice.B);
        }
        else if(InputType == UI_Input.DIALOGUE)
        {
            ConversationManager.Instance.NextLine();
        }
    }
}

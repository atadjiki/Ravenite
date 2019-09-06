using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public KeyCode Advance = KeyCode.Space;
    public KeyCode ChoiceA = KeyCode.A;
    public KeyCode ChoiceB = KeyCode.B;

    // Update is called once per frame
    void Update()
    {

        if (StateManager.Instance.Started)
        {
            if (Input.GetKeyDown(Advance))
            {
                if (ConversationManager.Instance.InConversation == false)
                {
                    ConversationManager.Instance.NextConversation();
                }
                else
                {
                    ConversationManager.Instance.NextLine();
                }

            }

            if (Input.GetKeyDown(ChoiceA))
            {
                ConversationManager.Instance.NextNode(Constants.Choice.A);
            }

            if (Input.GetKeyDown(ChoiceB))
            {
                ConversationManager.Instance.NextNode(Constants.Choice.B);
            }

        }

       
    }
}

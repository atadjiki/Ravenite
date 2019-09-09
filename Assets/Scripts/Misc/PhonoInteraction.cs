using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonoInteraction : MonoBehaviour
{
    void OnMouseDown()
    {
        GameState.Instance.TogglePhonoMode();
    }
}

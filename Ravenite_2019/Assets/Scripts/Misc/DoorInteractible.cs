using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractible : MonoBehaviour
{
    void OnMouseDown()
    {
        GameState.Instance.ToggleCredits();

    }
}

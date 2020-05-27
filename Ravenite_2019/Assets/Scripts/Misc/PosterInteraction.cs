using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterInteraction : MonoBehaviour
{
    void OnMouseDown()
    {
        GameState.Instance.TogglePosterMode();
    }
}

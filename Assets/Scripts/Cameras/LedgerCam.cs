﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgerCam : MonoBehaviour
{
    void OnMouseDown()
    {
        GameState.Instance.ToggleLedgerMode();
    }
}

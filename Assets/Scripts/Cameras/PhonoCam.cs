﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonoCam : MonoBehaviour
{
    void OnMouseDown()
    {
        CameraRig.Instance.ToggleStartCamera();
    }
}

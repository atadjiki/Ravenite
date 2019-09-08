using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonoInteraction : MonoBehaviour
{
    void OnMouseDown()
    {
        CameraRig.Instance.TogglePhonoCamera();
        UIManager.Instance.ToggleMusicSelectUI();
        //AudioManager.Instance.StartMusic();
    }
}

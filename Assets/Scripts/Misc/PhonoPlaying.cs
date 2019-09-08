using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonoPlaying : MonoBehaviour
{
    void OnMouseDown()
    {
        AudioManager.Instance.StartMusic();
    }
}

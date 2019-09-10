using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static KeyCode Advance = KeyCode.Space;
    public static KeyCode ChoiceA = KeyCode.A;
    public static KeyCode ChoiceB = KeyCode.B;
    public static KeyCode MainCam = KeyCode.Tab;
    public static KeyCode LedgerView = KeyCode.L;
    public static KeyCode MusicView = KeyCode.M;
    public static KeyCode Zoom = KeyCode.LeftShift;

    // Update is called once per frame
    void Update()
    {

        if (GameState.Instance.Started)
        {
            if (Input.GetKeyDown(Advance))
            {
                GameState.Instance.NextLine();
            }

            if (Input.GetKeyDown(ChoiceA))
            {
                GameState.Instance.NextChoice(Constants.Choice.A);
            }

            if (Input.GetKeyDown(ChoiceB))
            {
                GameState.Instance.NextChoice(Constants.Choice.B);
            }

            if (Input.GetKeyDown(MainCam))
            {
                GameState.Instance.SwitchToMainView();
            }

            if (Input.GetKeyDown(LedgerView))
            {
                GameState.Instance.ToggleLedgerMode();

            }

            if (Input.GetKeyDown(MusicView))
            {
                GameState.Instance.TogglePhonoMode();
            }

            if (Input.GetKey(Zoom))
            {

                GameState.Instance.CameraZoomIn();
            }
            else
            {
                GameState.Instance.CameraZoomOut();
            }

        }
    }
}

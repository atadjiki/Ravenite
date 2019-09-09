using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public KeyCode Advance = KeyCode.Space;
    public KeyCode ChoiceA = KeyCode.A;
    public KeyCode ChoiceB = KeyCode.B;
    public KeyCode MainCam = KeyCode.Tab;
    public KeyCode LedgerView = KeyCode.L;
    public KeyCode MusicView = KeyCode.M;
    public KeyCode Zoom = KeyCode.LeftShift;

    private void Awake()
    {
        Debug.Log(this.gameObject.name + " Initialized");
    }

    // Update is called once per frame
    void Update()
    {

        if (GameState.Instance.Started)
        {
            if (Input.GetKeyDown(Advance))
            {
                GameState.Instance.Next();
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

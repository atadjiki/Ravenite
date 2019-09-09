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
                CameraRig.Instance.SwitchToMain();
            }

            if (Input.GetKeyDown(LedgerView))
            {
                CameraRig.Instance.ToggleLedgerCamera();

            }

            if (Input.GetKey(Zoom))
            {
                 
                CameraRig.Instance.MainCameraZoomIn();
            }
            else
            {
                CameraRig.Instance.MainCameraZoomOut();
            }

        }
    }
}

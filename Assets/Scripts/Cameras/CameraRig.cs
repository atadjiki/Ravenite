using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRig : MonoBehaviour
{
    //Singleton vars
    private static CameraRig _instance;

    public static CameraRig Instance { get { return _instance; } }

    public CinemachineVirtualCamera Main;
    public CinemachineVirtualCamera Ledger;
    public CinemachineVirtualCamera Start;

    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        Build();
    }

    private void Build()
    {
        SwitchToStart();
    }

    public void SwitchToMain()
    {
        Ledger.enabled = false;
        Start.enabled = false;
        Main.enabled = true;
    }

    public void SwitchToLedger()
    {
        Main.enabled = false;
        Start.enabled = false;
        Ledger.enabled = true;
    }

    public void SwitchToStart()
    {
        Main.enabled = false;
        Ledger.enabled = false;
        Start.enabled = true;
    }

    public void ToggleLedgerCamera()
    {
        if (Ledger.enabled)
        {
            SwitchToMain();
        }
        else
        {
            SwitchToLedger();
        }
    }

    public void ToggleStartCamera()
    {
        if (Start.enabled)
        {
            SwitchToMain();
        }
        else
        {
            SwitchToStart();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchToMain();
        }
    }


}

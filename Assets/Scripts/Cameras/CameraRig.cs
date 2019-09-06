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
    public CinemachineVirtualCamera Gramophone;

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
        SwitchToMain();
    }

    public void SwitchToMain()
    {
        Ledger.enabled = false;
        Gramophone.enabled = false;
        Main.enabled = true;
    }

    public void SwitchToLedger()
    {
        Main.enabled = false;
        Gramophone.enabled = false;
        Ledger.enabled = true;
    }

    public void SwitchToGramophone()
    {
        Main.enabled = false;
        Ledger.enabled = false;
        Gramophone.enabled = true;
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

    public void ToggleGramophoneCamera()
    {
        if (Gramophone.enabled)
        {
            SwitchToMain();
        }
        else
        {
            SwitchToGramophone();
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

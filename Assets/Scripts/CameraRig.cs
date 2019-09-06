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

    }

    public void SwitchToMain()
    {
        Ledger.enabled = false;
        Main.enabled = true;
    }

    public void SwitchToLedger()
    {
        Ledger.enabled = true;
        Main.enabled = false;
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


}

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

    public float zoom_in_speed = 0.5f;
    public float zoom_out_speed = 1f;
    public float fov_max = 20;
    public float fov_min = 53;

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
        Debug.Log(this.gameObject.name + " Initialized");
    }

    private void Build()
    {
        Main.m_Lens.FieldOfView = fov_min;
        DisableAll();
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
        AudioManager.Instance.PlayLedger();
    }

    public void SwitchToStart()
    {
        Main.enabled = false;
        Ledger.enabled = false;
        Start.enabled = true;
    }

    public void DisableAll()
    {
        Main.enabled = false;
        Ledger.enabled = false;
        Start.enabled = false;
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

    public void MainCameraZoomIn()
    {
        
        if (Main.enabled)
        {
             Main.m_Lens.FieldOfView = Mathf.Lerp(Main.m_Lens.FieldOfView, fov_max, Time.deltaTime * zoom_in_speed);
        }
    }

    public void MainCameraZoomOut()
    {
        if (Main.enabled)
        {

            Main.m_Lens.FieldOfView = Mathf.Lerp(Main.m_Lens.FieldOfView, fov_min, Time.deltaTime * zoom_out_speed);
            
        }
    }

}

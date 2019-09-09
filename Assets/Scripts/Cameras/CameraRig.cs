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
    public CinemachineVirtualCamera Phono;
    public CinemachineVirtualCamera Start;
    public CinemachineVirtualCamera Credits;

    public float zoom_in_speed = 0.5f;
    public float zoom_out_speed = 1f;
    public float fov_max = 20;
    public float fov_min = 53;

    public float LerpTime = 2;
    public float Step = 0.1f;
    public float Current_Step = 0;
    public float Horizontal = 140;
    public float Vertical = 0;

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
        Main.m_Lens.FieldOfView = fov_min;
        DisableAll();
        SwitchToStart();
    }

    public void AllOff()
    {
        Ledger.enabled = false;
        Start.enabled = false;
        Phono.enabled = false;
        Main.enabled = false;
        Credits.enabled = false;
    }

    public void SwitchToMain()
    {
        AllOff();
        Main.enabled = true;
    }

    public void SwitchToLedger()
    {
        AllOff();
        Ledger.enabled = true;
    }

    public void SwitchToPhono()
    {
        AllOff();
        Phono.enabled = true;
    }

    public void SwitchToStart()
    {
        AllOff();
        Start.enabled = true;
    }

    public void SwitchToCredits()
    {
        AllOff();
        Credits.enabled = true;
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

    public void TogglePhonoCamera()
    {
        if (Phono.enabled)
        {
            SwitchToMain();
        }
        else
        {
            SwitchToPhono();
        }
    }

    public void ToggleCreditsCamera()
    {
        if (Credits.enabled)
        {
            SwitchToMain();
        }
        else
        {
            SwitchToCredits();
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

    public void LookAtCharacter()
    {
        if (Main.enabled)
        {
            StartCoroutine(LerpLookAt());
        }
    }

    IEnumerator LerpLookAt()
    {
        while(Current_Step < LerpTime)
        {

            Main.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value =
                Mathf.Lerp(Main.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value, Horizontal, Step);

            Main.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value =
                Mathf.Lerp(Main.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value, Vertical, Step);

            Current_Step += Step;
            yield return new WaitForEndOfFrame();
        }
    }

}

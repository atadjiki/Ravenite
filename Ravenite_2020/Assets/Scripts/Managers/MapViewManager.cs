using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapViewManager : MonoBehaviour
{
    private static MapViewManager _instance;
    public static MapViewManager Instance { get { return _instance; } }

    [SerializeField] private CinemachineVirtualCamera CM_Main = null;
    [SerializeField] private float axis_max_speed = 300;
    [SerializeField] private float fov_max = 70;
    [SerializeField] private float fov_min = 20;
    [SerializeField] private float fov_increment = 1;
    [SerializeField] private float fov_default = 50;


    private CinemachineOrbitalTransposer CM_OrbitalTransposer;
    private bool RotateMode = false;

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

    void Build()
    {
        //get orbital tranposer component from main camera
        CM_OrbitalTransposer = CM_Main.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        CM_OrbitalTransposer.m_XAxis.m_MaxSpeed = 0;

        //set default FOV
        CM_Main.m_Lens.FieldOfView = fov_default;
    }

    private void Update()
    {
        //handle rotating map 
        if(Input.GetMouseButtonDown(1))
        {
            CM_OrbitalTransposer.m_XAxis.m_MaxSpeed = axis_max_speed;
            RotateMode = true;
        }
        if(Input.GetMouseButtonUp(1))
        {
            CM_OrbitalTransposer.m_XAxis.m_MaxSpeed = 0;
            RotateMode = false;
        }

        //handle zoom in and zoom out
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            AdjustFOV(false);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            AdjustFOV(true);
        }
    }

    private void AdjustFOV(bool increment)
    {
        float current_fov = CM_Main.m_Lens.FieldOfView;

        if(increment)
        {
            //if the current fov is already at the max level
            if (current_fov >= fov_max)
            {
                CM_Main.m_Lens.FieldOfView = fov_max;
            }
            //if current fov + increment is still under the max
            else if ((current_fov + fov_increment) < fov_max)
            {
                CM_Main.m_Lens.FieldOfView += fov_increment;
            }
            //if incrementing will overflow, return max
            else if ((current_fov + fov_increment) > fov_max)
            {
                CM_Main.m_Lens.FieldOfView = fov_max;
            }
        }
        else 
        {
            if (current_fov <= fov_min)
            {
                CM_Main.m_Lens.FieldOfView = fov_max;
            }
            else if ((current_fov - fov_increment) > fov_min)
            {
                CM_Main.m_Lens.FieldOfView -= fov_increment;
            }
            else if ((current_fov - fov_increment) < fov_min)
            {
                CM_Main.m_Lens.FieldOfView = fov_min;
            }
        }

    }

    public bool IsRotating()
    {
        return RotateMode;
    }
}

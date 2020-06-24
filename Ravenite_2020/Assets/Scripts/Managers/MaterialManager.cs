using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    
    public enum Mat { Building, Selected };

    [SerializeField] private Material M_Building;
    [SerializeField] private Material M_Selected;

    private static MaterialManager _instance;
    public static MaterialManager Instance { get { return _instance; } }

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
    }

    public Material GetMaterial(Mat mat)
    {
        if(mat == Mat.Building)
        {
            return M_Building;
        }
        else if(mat == Mat.Selected)
        {
            return M_Selected;
        }
        else
        {
            return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class MaterialManager : MonoBehaviour
{
 
    [SerializeField] private Material M_Building_Neutral;
    [SerializeField] private Material M_Building_Player;
    [SerializeField] private Material M_Building_Enemy;
    [SerializeField] private Material M_Building_Police;
    [SerializeField] private Material M_Building_Selected;

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

    public Material GetMaterial(Materials.Asset asset)
    {
        if(asset == Materials.Asset.Building_Neutral)
        {
            return M_Building_Neutral;
        }
        else if (asset == Materials.Asset.Building_Player)
        {
            return M_Building_Player;
        }
        else if (asset == Materials.Asset.Building_Police)
        {
            return M_Building_Police;
        }
        else if (asset == Materials.Asset.Building_Enemy)
        {
            return M_Building_Enemy;
        }
        else if (asset == Materials.Asset.Building_Selected)
        {
            return M_Building_Selected;
        }
        else
        {
            return null;
        }
    }
}

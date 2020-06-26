using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class MaterialManager : MonoBehaviour
{
 
    [Header("Materials")]
    [SerializeField] private Material M_Building_Selectable;
    [SerializeField] private Material M_Building_Unselectable;
    [SerializeField] private Material M_Selected;

    [Header("Colors")]
    [SerializeField] private Color Color_Player;
    [SerializeField] private Color Color_Neutral;
    [SerializeField] private Color Color_Police;
    [SerializeField] private Color Color_Unselectable;
    
    [Header("Enemy Colors")]
    [SerializeField] private Color Color_Enemy_1;
    [SerializeField] private Color Color_Enemy_2;
    [SerializeField] private Color Color_Enemy_3;
    [SerializeField] private Color Color_Enemy_4;

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

    public Material GetBuildingMaterial()
    {
        return M_Building_Selectable;
    }

    public Material GetSelectedMaterial()
    {
        return M_Selected;
    }

    public Material GetMaterialByFlag(Buildings.Flag flag)
    {
        if(flag == Buildings.Flag.Selectable)
        {
            return M_Building_Selectable;
        }
        else if(flag == Buildings.Flag.Unselectable)
        {
            return M_Building_Unselectable;
        }
        else
        {
            return null;
        }
    }

    public Color GetColorByAsset(Materials.Asset asset)
    {
        if(asset == Materials.Asset.Building_Neutral)
        {
            return Color_Neutral;
        }
        else if (asset == Materials.Asset.Building_Player)
        {
            return Color_Player;
        }
        else if (asset == Materials.Asset.Building_Police)
        {
            return Color_Police;
        }
        else if (asset == Materials.Asset.Building_Enemy_1)
        {
            return Color_Enemy_1;
        }
        else if (asset == Materials.Asset.Building_Enemy_2)
        {
            return Color_Enemy_2;
        }
        else if (asset == Materials.Asset.Building_Enemy_3)
        {
            return Color_Enemy_3;
        }
        else if (asset == Materials.Asset.Building_Enemy_4)
        {
            return Color_Enemy_4;
        }
        else
        {
            return Color.white;
        }
    }
}

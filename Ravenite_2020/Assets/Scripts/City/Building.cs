using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[RequireComponent(typeof(Collider))]
public class Building : MonoBehaviour
{
    [SerializeField] private Factions.Faction faction = Factions.Faction.Neutral;
    [SerializeField] private Buildings.Flag flag = Buildings.Flag.Unselectable;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = MaterialManager.Instance.GetMaterialByFlag(flag);

        if (flag == Buildings.Flag.Selectable)
        {
            
            _renderer.material.color = MaterialManager.Instance.GetColorByAsset(Materials.BuildingToMaterial(faction));
        }
        
        if(TerritoryManager.Instance != null)
        {
            if (faction != Factions.Faction.Neutral)
            {
                TerritoryManager.Instance.SetTilesByBuilding(this);
            }
            
        }
        
    }

    private void OnMouseOver()
    {
        if(flag == Buildings.Flag.Selectable && MapViewManager.Instance.IsRotating() == false)
        {
            _renderer.material = MaterialManager.Instance.GetSelectedMaterial();
            _renderer.material.color = MaterialManager.Instance.GetColorByAsset(Materials.BuildingToMaterial(faction));
            _renderer.material.color = new Color(_renderer.material.color.r * 2f, _renderer.material.color.g * 2f, _renderer.material.color.b * 2f, 0.75f);
        }
    }

    private void OnMouseExit()
    {
        if(flag == Buildings.Flag.Selectable)
        {
            _renderer.material = MaterialManager.Instance.GetBuildingMaterial();
            _renderer.material.color = MaterialManager.Instance.GetColorByAsset(Materials.BuildingToMaterial(faction));
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on " + name + " - " + faction + " - " + flag);
    }
}

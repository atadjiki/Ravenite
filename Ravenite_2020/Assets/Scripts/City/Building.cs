using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[RequireComponent(typeof(Collider))]
public class Building : MonoBehaviour
{
    [SerializeField] private Buildings.Type type = Buildings.Type.Neutral;
    [SerializeField] private Buildings.Flag flag = Buildings.Flag.Unselectable;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = MaterialManager.Instance.GetMaterial(Materials.BuildingToMaterial(type));

        if(type != Buildings.Type.Neutral)
        {
            
        }
        TerritoryManager.Instance.SetTilesByBuilding(this);
    }

    private void OnMouseOver()
    {
        if(flag == Buildings.Flag.Selectable && MapViewManager.Instance.IsRotating() == false)
        {
            _renderer.material = MaterialManager.Instance.GetMaterial(Materials.Asset.Building_Selected);
        }
    }

    private void OnMouseExit()
    {
        if(flag == Buildings.Flag.Selectable)
        {
            _renderer.material = MaterialManager.Instance.GetMaterial(Materials.BuildingToMaterial(type));
        }
    }
}

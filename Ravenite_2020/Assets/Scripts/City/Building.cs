using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[RequireComponent(typeof(Collider))]
public class Building : MonoBehaviour
{
    [SerializeField] private Buildings.Type type = Buildings.Type.Neutral;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = MaterialManager.Instance.GetMaterial(Materials.BuildingToMaterial(type));
    }

    private void OnMouseOver()
    {
        if(MapViewManager.Instance.IsRotating() == false)
        {
            _renderer.material = MaterialManager.Instance.GetMaterial(Materials.Asset.Building_Selected);
        }
    }

    private void OnMouseExit()
    {
        _renderer.material = MaterialManager.Instance.GetMaterial(Materials.BuildingToMaterial(type));
    }
}

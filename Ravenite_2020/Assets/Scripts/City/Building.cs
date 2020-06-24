using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Building : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = MaterialManager.Instance.GetMaterial(MaterialManager.Mat.Building);
    }

    private void OnMouseOver()
    {
        if(MapViewManager.Instance.IsRotating() == false)
        {
            _renderer.material = MaterialManager.Instance.GetMaterial(MaterialManager.Mat.Selected);
        }
    }

    private void OnMouseExit()
    {
        _renderer.material = MaterialManager.Instance.GetMaterial(MaterialManager.Mat.Building);
    }
}

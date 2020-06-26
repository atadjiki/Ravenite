using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerritoryManager : MonoBehaviour
{
    private static TerritoryManager _instance;
    public static TerritoryManager Instance { get { return _instance; } }

    private HashSet<TerritoryTile> Tiles;

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
        Tiles = new HashSet<TerritoryTile>(GetComponentsInChildren<TerritoryTile>());
    }

    public void SetTilesByBuilding(Building building)
    {
        List<TerritoryTile> overlapping = new List<TerritoryTile>();

        foreach(Collider c in Physics.OverlapSphere(building.transform.position, 1.5f))
        {
            if(c.GetComponent<TerritoryTile>())
            {
                overlapping.Add(c.GetComponent<TerritoryTile>());
            }
        }

        foreach(TerritoryTile tile in overlapping)
        {
            Color buildingColor = building.gameObject.GetComponent<Renderer>().material.color;
            float modifier = 0.7f;

            // You can cache a reference to the renderer to avoid searching for it.
            tile.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(buildingColor.r * modifier, buildingColor.g * modifier, buildingColor.b * modifier, modifier));
        }
    }
}

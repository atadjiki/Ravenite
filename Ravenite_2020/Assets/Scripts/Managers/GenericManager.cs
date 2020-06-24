using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericManager : MonoBehaviour
{
    private static GenericManager _instance;
    public static GenericManager Instance { get { return _instance; } }

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
}

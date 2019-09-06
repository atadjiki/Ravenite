using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgerCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        Debug.Log("Ledger clicked");
        CameraRig.Instance.ToggleLedgerCamera();
    }
}

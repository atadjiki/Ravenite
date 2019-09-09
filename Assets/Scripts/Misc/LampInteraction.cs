using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampInteraction : MonoBehaviour
{
    LightFlickeringEffect effect;

    private void Start()
    {
        effect = GetComponentInChildren<LightFlickeringEffect>();
    }

    void OnMouseDown()
    {
        GameState.Instance.DoLamp();
        
    }

    public void PlayEffect()
    {
        effect.Swichting();
    }
}

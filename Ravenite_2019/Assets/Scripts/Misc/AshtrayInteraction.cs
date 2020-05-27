using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshtrayInteraction : MonoBehaviour
{
    public GameObject Cigarette;
    private CigaretteInteraction interaction;
    public ParticleSystem SmokeEffect;
    void OnMouseDown()
    {
        GameState.Instance.DoCigaretteEffect();
        
    }

    public void PlayEffect()
    {
        interaction = Cigarette.GetComponent<CigaretteInteraction>();
        interaction.PlayAnimation();
        SmokeEffect.Play();
    }
}

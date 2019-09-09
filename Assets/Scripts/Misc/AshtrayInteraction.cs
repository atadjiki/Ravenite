using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshtrayInteraction : MonoBehaviour
{
    public GameObject Cigarette;
    private CigaretteInteraction interaction;
    void OnMouseDown()
    {
        //GameState.Instance.TogglePhonoMode();
        AudioManager.Instance.PlayClick();
        interaction = Cigarette.GetComponent<CigaretteInteraction>();
        interaction.PlayAnimation();
    }
}

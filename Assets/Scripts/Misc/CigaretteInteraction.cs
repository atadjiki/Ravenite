using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigaretteInteraction : MonoBehaviour
{
    public float intensity;

    private GameObject smokeLightObj;
    private Light smokeLight;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        smokeLightObj = transform.GetChild(0).gameObject;
        smokeLight = smokeLightObj.GetComponent<Light>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        smokeLight.intensity = intensity;
    }

    public void PlayAnimation()
    {
        anim.Play("Smoke");
    }
}

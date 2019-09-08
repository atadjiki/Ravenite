using UnityEngine;
using System.Collections;

public class LightFlickeringEffect : MonoBehaviour
{

    public float MinWaitTime = 0.05f;
    public float MaxWaitTime = 0.5f;
    public float AnimationLength = 3.0f;

    private Light[] lights;
    private bool state = false;
    private float timer;

    void Start()
    {
        lights = GetComponentsInChildren<Light>();
    }

    public void Swichting()
    {
        timer = 0.0f;
        state = !state;
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing()
    {
        foreach (Light light in lights)
        {
            light.enabled = !light.enabled;
        }

        while (timer < AnimationLength)
        {
            float elapsed = Random.Range(MinWaitTime, MaxWaitTime);
            yield return new WaitForSeconds(elapsed);
            foreach(Light light in lights)
            {
                light.enabled = !light.enabled;
            }
            timer += (Time.deltaTime + elapsed);
        }

        foreach (Light light in lights)
        {
            light.enabled = state;  
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    public float MinLightPower = 0.8f;
    public float MaxLightPower = 2.5f;
    public float MinWaitTime = 0.1f;
    public float MaxWaitTime = 0.7f;
    public float SmoothTime = 0.1f;
    private float LightPower;
    private Light Fire;

    void Start()
    {
        Fire = GetComponent<Light>();
        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        while (true)
        {
            LightPower = Random.Range(MinLightPower,MaxLightPower);
            yield return new WaitForSeconds(Random.Range(MinWaitTime, MaxWaitTime));
        }
    }

    void Update()
    {
        Fire.intensity = Mathf.SmoothStep(Fire.intensity, LightPower, SmoothTime);
    }
}
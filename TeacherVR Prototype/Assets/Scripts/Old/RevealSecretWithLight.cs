using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealSecretWithLight : MonoBehaviour
{
    public Transform RevealingLight;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (RevealingLight)
        {
            rend.material.SetVector("_LightPos", RevealingLight.position);
            rend.material.SetVector("_LightDir", RevealingLight.forward);
        }
    }
}

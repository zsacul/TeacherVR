using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildOnce : MonoBehaviour
{

    public Material material;

    public float maxY;
    public float duration;

    private float y;

    void Start()
    {
        y = transform.position.y-0.3f;
        material.SetFloat("_ConstructY", y);
    }
    void Update()
    {
        y = Mathf.Lerp(y, transform.position.y*maxY, duration);
        material.SetFloat("_ConstructY", y);
    }
}
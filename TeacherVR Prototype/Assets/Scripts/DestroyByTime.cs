using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float LifeTime;
    public bool SnapDropZone = false;
    public string SnapObjectName;

    private bool wasKinematic = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!SnapDropZone) Destroy(gameObject, LifeTime);
    }

    void Update()
    {
        if (rb != null)
            if (rb.isKinematic)
            {
                wasKinematic = true;
            }
            else if(wasKinematic)
            {
                Destroy(gameObject, LifeTime);
            }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_InteractableObject))]
public class ResetOnRange : MonoBehaviour
{
    public float MaxRange = 10;
    public bool DoubleConnect = false;
    public Transform End;

    private VRTK_InteractableObject io;
    private VRTK_InteractableObject io2;

    void Start()
    {
        io = GetComponent<VRTK_InteractableObject>();
        if (DoubleConnect) io2 = End.GetComponent<VRTK_InteractableObject>();
    }

    void Update()
    {
        if (Vector3.Distance(End.position, transform.position) > MaxRange)
        {
            io.ForceStopInteracting();
            if (DoubleConnect)
            {
                io2.ForceStopInteracting();
                Vector3 a;
                Vector3 b;
                a = Vector3.Lerp(transform.position, End.position, 1f / 3);
                b = Vector3.Lerp(transform.position, End.position, 2f / 3);
                transform.position = a;
                End.position = b;
            }
            else transform.position = End.position;
        }
    }
}
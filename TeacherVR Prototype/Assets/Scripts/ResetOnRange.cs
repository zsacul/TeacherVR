using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_InteractableObject))]
public class ResetOnRange : MonoBehaviour
{
    public float MaxRange = 10;

    private Vector3 startPos;
    private VRTK_InteractableObject io;

    void Start ()
	{
	    startPos = transform.position;
	    io = GetComponent<VRTK_InteractableObject>();
	}
	
	void Update ()
    {
        if (Vector3.Distance(startPos, transform.position) > MaxRange)
        {
            io.ForceStopInteracting();
            transform.position = startPos;
        }
	}
}

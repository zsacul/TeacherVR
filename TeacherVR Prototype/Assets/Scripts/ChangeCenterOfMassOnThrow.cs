using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChangeCenterOfMassOnThrow : MonoBehaviour
{
    public Vector3 CenterOfMass;

    private Rigidbody rb;

	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += ChangeCenterOfMassOnThrow_InteractableObjectUngrabbed;
	}

    private void ChangeCenterOfMassOnThrow_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        rb.centerOfMass = CenterOfMass;
    }

    void Reset()
    {
        rb.ResetCenterOfMass();
    }

    private void OnCollisionEnter(Collision col)
    {
        Reset();
    }
}

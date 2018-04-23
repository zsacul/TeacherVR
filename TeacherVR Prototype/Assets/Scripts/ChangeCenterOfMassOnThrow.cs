using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChangeCenterOfMassOnThrow : MonoBehaviour
{
    public Vector3 CenterOfMass;

    private Rigidbody rb;
    private GameObject model;

	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
	    model = transform.Find("Model").gameObject;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += ChangeCenterOfMassOnThrow_InteractableObjectUngrabbed;
	}

    private void ChangeCenterOfMassOnThrow_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        model.transform.position = model.transform.position + CenterOfMass - rb.centerOfMass;
        rb.centerOfMass = CenterOfMass;
        
    }

    void Reset()
    {
        model.transform.position = Vector3.zero;
        rb.ResetCenterOfMass();
    }

    private void OnCollisionEnter(Collision col)
    {
        Reset();
    }
}

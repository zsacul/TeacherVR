using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChangeCenterOfMassOnThrow : MonoBehaviour
{
    public Vector3 CenterOfMass;

    private Rigidbody rb;
    private GameObject model;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        model = transform.Find("Model").gameObject;
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed +=
            ChangeCenterOfMassOnThrow_InteractableObjectGrabbed;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed +=
            ChangeCenterOfMassOnThrow_InteractableObjectUngrabbed;
    }

    private void ChangeCenterOfMassOnThrow_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        Restart();
    }

    private void ChangeCenterOfMassOnThrow_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        model.transform.localPosition = model.transform.localPosition + CenterOfMass - rb.centerOfMass;
        rb.centerOfMass = CenterOfMass;
    }

    void Restart()
    {
        model.transform.localPosition = Vector3.zero;
        rb.ResetCenterOfMass();
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag == "Egg") Destroy(gameObject,0.7f);
        Restart();
    }
}
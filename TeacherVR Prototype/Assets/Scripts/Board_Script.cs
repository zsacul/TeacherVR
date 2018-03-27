using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Board_Script : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<MeshCollider>();
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += ObjectGrabbed;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += ObjectUngrabbed;
    }

    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        col.enabled = false;
    }

    private void ObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        StartCoroutine(wait());
    }

    private IEnumerator wait()
    {
        rb.isKinematic = false;
        yield return new WaitForSeconds(1f);
        rb.isKinematic = true;
        col.enabled = true;
    }
}
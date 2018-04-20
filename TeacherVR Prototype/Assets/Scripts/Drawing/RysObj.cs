using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RysObj : MonoBehaviour
{
    VRTK_InteractableObject io;
    VRTK_Pointer po;
    VRTK_StraightPointerRenderer spr;

    void Start()
    {
        io = GetComponentInParent<VRTK_InteractableObject>();
        po = GetComponent<VRTK_Pointer>();
        spr = GetComponent<VRTK_StraightPointerRenderer>();
        io.InteractableObjectGrabbed += ObjectGrabbed;
        io.InteractableObjectUngrabbed += ObjectUngrabbed;
    }

    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        spr.enabled = true;
        po.enabled = true;
        GameController.Instance.DrawingManager.RysObject = gameObject;
    }


    private void ObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        po.enabled = false;
        spr.enabled = false;
        GameController.Instance.DrawingManager.RysObject = null;
    }
}
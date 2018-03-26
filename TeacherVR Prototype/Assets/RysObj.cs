using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RysObj : MonoBehaviour {
    Rigidbody rb;
    VRTK.VRTK_Pointer po;
    VRTK.VRTK_StraightPointerRenderer spr;

	void Start ()
    {
        rb = GetComponentInParent<Rigidbody>();
        po = GetComponent<VRTK.VRTK_Pointer>();
        spr = GetComponent<VRTK.VRTK_StraightPointerRenderer>();
	}

    void Update()
    {
        if (rb.isKinematic)
        {
            po.enabled = true;
            spr.enabled = true;
            GameController.Instance.RysObject = gameObject;
        }
        else
        {
            spr.enabled = false;
            po.enabled = false;
        }
    }
}

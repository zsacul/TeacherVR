using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BreakForceChangeDelay : MonoBehaviour
{
    
	void Start ()
    {
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += BreakForceChangeDelay_InteractableObjectGrabbed;
	}

    private void BreakForceChangeDelay_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        StartCoroutine(Force());
    }

    IEnumerator Force()
    {
        yield return new WaitForSeconds(0.5f);
        var tmp = GetComponent<FixedJoint>();
        if(tmp!=null)tmp.breakForce = 100;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class SnapToDrawing : MonoBehaviour {

    bool grabbed;
    bool nearBoard;
    public Vector3 Sta;
    public Vector3 End;
    // Use this for initialization
    void Start () {
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed +=
           ChangeCenterOfMassOnThrow_InteractableObjectGrabbed;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed +=
    ChangeCenterOfMassOnThrow_InteractableObjectUngrabbed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "HitBoxBoard")
        {
          //  collision.gameObject
            nearBoard = true;
        }
    }

    // Update is called once per frame
    void LateUpdate () {
		if(grabbed && nearBoard)
        {
                transform.localPosition = new Vector3(
                Mathf.Clamp(transform.localPosition.x, Sta.x, End.x),
               transform.localPosition.y,
               transform.localPosition.z);
            nearBoard = false;
        }
	}
    private void ChangeCenterOfMassOnThrow_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        grabbed = true;
    }
    private void ChangeCenterOfMassOnThrow_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        grabbed = false;
    }


}

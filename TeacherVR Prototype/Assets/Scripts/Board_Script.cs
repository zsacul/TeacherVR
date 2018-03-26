using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Board_Script : MonoBehaviour {

	public Rigidbody rb;
	private float wait_a_sec = 2.0F;
	private float timer = 0.0F;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);
	}
	
	// Update is called once per frame
	void Update () {
		if (rb.IsSleeping()) {
			if (timer == 0.0F){
				timer = Time.time;
			}
			
			if ((Time.time - timer) > wait_a_sec ){
				rb.isKinematic = false;
				Debug.Log("isKinematic = false");
				timer = 0.0F;
			}
			
		}
	}

	private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)

    {

        rb.isKinematic = true;
        Debug.Log("isKinematic = true");

    }
}

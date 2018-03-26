using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Tutorial_Point_Spawn_Control : MonoBehaviour {

	// Use this for initialization

	public Tutorial_Point_Anim_Control tutorial_point_user;


	void Start() {
		GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);
	}
	

	private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)

    {
        Debug.Log("chalk grabbed");
    	tutorial_point_user.Kill();

    }
}

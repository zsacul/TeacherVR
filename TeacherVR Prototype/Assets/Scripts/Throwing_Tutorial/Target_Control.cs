using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Control : MonoBehaviour {

	
	private bool state = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Targets(){
		foreach (Transform child in transform) {
			state = true;

		}
	}
}

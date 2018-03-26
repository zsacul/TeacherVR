using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour {

	public GameObject frame_01;
	public GameObject frame_02;

	private Renderer rend_01;
	private Renderer rend_02;

	void Start(){
		rend_01 = frame_01.GetComponent<Renderer>();
		rend_02 = frame_02.GetComponent<Renderer>();

	}

	void Update () {
		bool number = Mathf.FloorToInt(Time.time) % 2 == 0;

		rend_01.enabled = number;
		rend_02.enabled = !number; 
	}
}

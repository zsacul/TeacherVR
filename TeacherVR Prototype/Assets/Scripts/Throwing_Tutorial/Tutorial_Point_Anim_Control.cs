using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Point_Anim_Control : MonoBehaviour {

	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	

	public void Kill(){
		anim.SetBool("Alive", false);
		//Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + 2.0F);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalk_Hit : MonoBehaviour
{
	private Renderer rend;
	private bool state = true;

	public Tutorial_Point_Anim_Control tutorial_point_user;

	void Start() {
		rend = transform.GetComponent<Renderer>();
	}

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Chalk"))
        {
        	if (state) {
        		rend.material.color = Color.red;
        		state = false;
        		} else {
        		rend.material.color = Color.yellow;
        		tutorial_point_user.Kill();
        		}
            
            
        }

    }
}

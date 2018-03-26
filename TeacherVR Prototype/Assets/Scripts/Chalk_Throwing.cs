using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalk_Throwing : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Chalk"))
        {
        	
        	transform.GetComponent<Renderer>().material.color = Color.red;
        }

    }
}

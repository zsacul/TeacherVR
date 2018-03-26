using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalk_Throwing : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Chalk"))
        {
        	
        	rend.material.color = Color.red;
        }

    }
}

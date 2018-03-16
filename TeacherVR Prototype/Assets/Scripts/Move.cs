using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public GameObject deska;
    private Vector3 offset;
    
	// Use this for initialization
	void Start () {
        offset = transform.position - deska.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position = deska.transform.position + offset;
    }
}

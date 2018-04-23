using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTransformByVectorFromPoint : MonoBehaviour
{
    public Transform Point;
    private Vector3 vec;

	void Start ()
	{
	    vec = transform.position - Point.position;
	}
	
	void Update ()
	{
	    transform.position = Point.position + vec;
	}
}

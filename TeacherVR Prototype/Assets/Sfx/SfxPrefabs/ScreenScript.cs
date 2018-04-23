using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScript : MonoBehaviour {

    public AudioSource LowerSource;
    public AudioSource RaiseSource;


    public void SLower()
    {
        LowerSource.Play();
    }

    public void SRaise()
    {
        RaiseSource.Play();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

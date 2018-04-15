using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDoorScript : MonoBehaviour {

    public AudioSource BangSource;
    public AudioSource OpenSource;


    public void SOpen()
    {
        OpenSource.Play();
    }

    public void SBang()
    {
        BangSource.Play();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

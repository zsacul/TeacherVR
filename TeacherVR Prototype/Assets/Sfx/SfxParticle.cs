using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxParticle : MonoBehaviour {

    private int _cnt = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(_cnt % 100 == 0)
        {
            gameObject.SetActive(GetComponent<AudioSource>().isPlaying);
            _cnt = 1;
        }

        _cnt++;
	}
}

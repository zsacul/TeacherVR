using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSfxPrefabScript : MonoBehaviour {

    public AudioSource _source;

	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {

        //TODO something smarter!
        this.gameObject.SetActive(_source.isPlaying);
		
	}
}

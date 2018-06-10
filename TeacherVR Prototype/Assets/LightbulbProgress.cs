using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightbulbProgress : MonoBehaviour {

    public int ToLearn;
    public int Seen;
	// Use this for initialization
	void Start () {
        ToLearn = 15;
        Seen = ToLearn;
	}
	
	// Update is called once per frame
	void Update () {
        if(Seen== 0)
        {

            gameObject.transform.parent.GetComponentInChildren<AnimationControll>().Clap();
            gameObject.transform.parent.GetComponentInChildren<AnimationControll>().Talk(false);
            Lecture.remainingStudents--;
            Seen = -1;
            ToLearn = -1;
            Destroy(gameObject, 2f);
                
        }
        else
         if(Seen!=ToLearn)
        {
            Seen--;
            Debug.Log("Uczy sie");
        }
	}
}

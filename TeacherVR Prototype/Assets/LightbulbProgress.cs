using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightbulbProgress : MonoBehaviour {

    public float ToLearn;
    public float Seen;
    private float total;
    public Material light_mat;

	void Start () {
        ToLearn = 15;
        Seen = ToLearn;
        total = ToLearn;
	}
	

	void Update () {
        if(Seen== 0)
        {
            transform.GetChild(4).gameObject.GetComponent<MeshRenderer>().material = light_mat;
            gameObject.transform.parent.GetComponentInChildren<AnimationControll>().Clap();
            gameObject.transform.parent.GetComponentInChildren<AnimationControll>().Talk(false);
            Lecture.remainingStudents--;
            GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.FiftyPoints, gameObject.transform.position);
            Seen = -1;
            ToLearn = -1;
            Destroy(gameObject, 2f);
                
        }
        else
         if(Seen!=ToLearn)
        {
            Seen--;
            for (int i = 0; i < 80*(total- Seen)/total; i+=20)
            {
               // Debug.Log(transform.childCount - 1 + " " + (Mathf.FloorToInt(i / 20)));
                if (transform.childCount-1 >= (Mathf.FloorToInt(i / 20)))
                transform.GetChild(Mathf.FloorToInt(i/20)).gameObject .GetComponent<MeshRenderer>().material = light_mat;
            }

        }
	}
}

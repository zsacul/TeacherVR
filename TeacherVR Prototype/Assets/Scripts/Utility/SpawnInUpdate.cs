using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInUpdate : MonoBehaviour
{
    public GameObject obj;
    public float delay;

    private float lastTime;
	void Update () {
	    if (Time.time > lastTime + delay && gameObject.activeSelf)
	    {
	        lastTime = Time.time;
	        GameObject tmp = Instantiate(obj, transform.position, obj.transform.rotation);
	        tmp.SetActive(true);
	    }
	}
}

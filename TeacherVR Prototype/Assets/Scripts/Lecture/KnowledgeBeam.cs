using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class KnowledgeBeam : MonoBehaviour {

    public Vector3 force;
    private Rigidbody rb;
    private GameObject Headset;
	// Use this for initialization
	void Start () {
        
        Headset = VRTK_DeviceFinder.HeadsetTransform().gameObject;
        Debug.Log(Headset);
        force = new Vector3(0, 0, 0);
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = gameObject.transform.forward * 6;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.CompareTag("Egg"))
        {
            Debug.Log("+1 knowledge");
            if (collision.transform.parent.parent.GetComponentInChildren<LightbulbProgress>() != null)
            {
                collision.transform.parent.parent.GetComponentInChildren<LightbulbProgress>().ToLearn--;
            }
            else
                Debug.Log("null");

            Destroy(gameObject);
        }
        else 
            Destroy(gameObject);
    }
}

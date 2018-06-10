using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class KnowledgeBeam : MonoBehaviour {

    public Vector3 force;
    private Rigidbody rb;
    public float speed;
    public Mesh[] smartThings;
    private GameObject Headset;
	// Use this for initialization
	void Start () {
        int model = Random.Range(0, 14);
        GetComponentInChildren<MeshFilter>().sharedMesh = smartThings[model];
        Headset = VRTK_DeviceFinder.HeadsetTransform().gameObject;
        Debug.Log(Headset);
        force = new Vector3(0, 0, 0);
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = gameObject.transform.forward * speed;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class KnowledgeBeam : MonoBehaviour {



    private Rigidbody rb;
    public float speed;
    public Mesh[] smartThings;

    public float lifetime;
    private float lifeSpawnTime;

	// Use this for initialization
	void Start () {

        int model = Random.Range(0, 14);
        GetComponentInChildren<MeshFilter>().sharedMesh = smartThings[model];

	    lifeSpawnTime = Time.time;

        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = gameObject.transform.forward * speed;
	}

     void Update()
     {
         if (Time.time - lifeSpawnTime >= .5f)
             gameObject.GetComponent<SphereCollider>().enabled=true;
        if (lifeSpawnTime + lifetime <= Time.time)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.CompareTag("Egg"))
        {
 
            if (collision.transform.parent.parent.GetComponentInChildren<LightbulbProgress>() != null)
            {
                collision.transform.parent.parent.GetComponentInChildren<LightbulbProgress>().ToLearn--;
            }


            Destroy(gameObject);
        }
        else 
            Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollDestroy : MonoBehaviour
{
    public GameObject WaterSplash;
    public bool trigger = false;

    void OnCollisionExit(Collision col)
    {
        Instantiate(WaterSplash, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        Instantiate(WaterSplash, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Destroy(this.gameObject);
    }

    void OnCollisionStay(Collision col)
    {
        Instantiate(WaterSplash, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (trigger)
        {
            Instantiate(WaterSplash, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
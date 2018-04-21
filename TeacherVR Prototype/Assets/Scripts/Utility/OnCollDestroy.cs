using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollDestroy : MonoBehaviour
{
    public GameObject WaterSplash;
    public bool trigger = false;

    void OnCollisionExit(Collision col)
    {
        GameController.Instance.SoundManager.Play3DAt(SamplesList.WaterSplash, transform.position);
        Instantiate(WaterSplash, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        GameController.Instance.SoundManager.Play3DAt(SamplesList.WaterSplash, transform.position);
        Instantiate(WaterSplash, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Destroy(this.gameObject);
    }

    void OnCollisionStay(Collision col)
    {
        GameController.Instance.SoundManager.Play3DAt(SamplesList.WaterSplash, transform.position);
        Instantiate(WaterSplash, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (trigger)
        {
            GameController.Instance.SoundManager.Play3DAt(SamplesList.WaterSplash, transform.position);
            Instantiate(WaterSplash, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public GameObject WaterSplash;

    void OnTriggerEnter(Collider col)
    {
        if (col.name != "BoxColliderToIgnore")
        {
            GameController.Instance.SoundManager.Play3DAt(SamplesList.WaterSplash, transform.position, 0.02f);
            Instantiate(WaterSplash, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}